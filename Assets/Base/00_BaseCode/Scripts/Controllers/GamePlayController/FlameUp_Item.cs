using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FlameUp_Item : MonoBehaviour
{
    public int num;
    public Text tvNum;
    public GameObject icon;
    public Button btnFastBoom;
    public void Init()
    {
        if(UseProfile.FlameUp_Item >= 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.FlameUp_Item;
        }
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.FLAMEUP_ITEM, HandleShowFlameUp);
    }

    public void HandleShowFlameUp(object param)
    {
      
        if (UseProfile.FlameUp_Item >= 1)
        {
          
            icon.SetActive(true);
            icon.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(delegate {
                icon.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            });

            tvNum.text = "" + UseProfile.FlameUp_Item;
        }
        else
        {
            icon.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f).OnComplete(delegate {
                icon.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(delegate {
                    icon.gameObject.SetActive(false);
                });
            });
        }
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
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.FLAMEUP_ITEM, HandleShowFlameUp);
    }
}
