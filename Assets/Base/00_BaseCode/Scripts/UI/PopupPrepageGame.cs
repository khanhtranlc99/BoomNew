using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class PopupPrepageGame : MonoBehaviour
{
    public Text tvLevel;
    public Text tvTotoScore;
    public CanvasGroup canvasGroup;
    public GameObject parent;
    public AudioClip tingTing;
    public void Init(Action callBack)
    {
        tvLevel.text = "LEVEL " + UseProfile.CurrentLevel;
      
        parent.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack).OnComplete(delegate {

          
            HandleOff();
            callBack?.Invoke();
        });

  
   
    }
    public void HandleOff()
    {
       
      
        canvasGroup.DOFade(0, 0.5f).SetDelay(1.5f).OnComplete(delegate {
         
            this.gameObject.SetActive(false);

            GameController.Instance.musicManager.PlayOneShot(tingTing);
        });
    }


}
