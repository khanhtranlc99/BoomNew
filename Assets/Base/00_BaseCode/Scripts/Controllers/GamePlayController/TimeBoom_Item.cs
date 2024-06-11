using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class TimeBoom_Item : MonoBehaviour
{

    public int num;
    public Text tvNum;
    public GameObject icon;

    public void Init()
    {
        if (UseProfile.TimeBoom_Item >= 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.TimeBoom_Item;
        }
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.TIMEBOOM_ITEM, HandleShowTimeBoom_Itemp);
    }

    public void HandleShowTimeBoom_Itemp(object param)
    {
        if (UseProfile.TimeBoom_Item >= 1)
        {
           
            icon.SetActive(true);
            icon.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(delegate {
                icon.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            });
            tvNum.text = "" + UseProfile.TimeBoom_Item;
        }
    }
    public void ShowIcon(Action callBack)
    {
        if(!icon.gameObject.activeSelf)
        {
            icon.transform.localScale = Vector3.zero;
            icon.SetActive(true);
            icon.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(delegate {
               
                icon.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate {
                    if (callBack != null)
                    {
                        callBack?.Invoke();
                    }

                });
            });
        }
        else
        {
            if (callBack != null)
            {
                callBack?.Invoke();
            }
        }


    }
    private void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.TIMEBOOM_ITEM, HandleShowTimeBoom_Itemp);
    }
}
