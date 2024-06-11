using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FastBoom_Item : MonoBehaviour
{
    public int num;
    public Text tvNum;
    public GameObject icon;

    public void Init()
    {
        if (UseProfile.FastBoom_Item >= 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.FastBoom_Item;
        }
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.FASTBOOM_ITEM, HandleShowFastBoom_Item);
    }

    public void HandleShowFastBoom_Item(object param)
    {
        if (UseProfile.FastBoom_Item >= 1)
        {
          
            icon.SetActive(true);
            icon.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(delegate {
                icon.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            });
            tvNum.text = "" + UseProfile.FastBoom_Item;
        }
        Debug.LogError("HandleShowFastBoom_Item_" + UseProfile.FastBoom_Item);
    }

    public void ShowIcon(Action callBack)
    {
        if (!icon.gameObject.activeSelf)
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
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.FASTBOOM_ITEM, HandleShowFastBoom_Item);
    }
}
