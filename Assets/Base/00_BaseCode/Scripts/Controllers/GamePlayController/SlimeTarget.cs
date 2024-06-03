using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlimeTarget : MonoBehaviour
{
    public SlimeType slimeType; 
    public Transform icon;
    public Text tvCount;
    public GameObject objComplete;
    public bool isComplete;
    public int count;
    

    public void Init (int param)
    {
        isComplete = false;
        tvCount.text = "" + param;
        count = param;
      
    }

    public void HandleSubtraction()
    {
        icon.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.35f).OnComplete(delegate {
            icon.transform.DOScale(new Vector3(1, 1, 1), 0.35f).OnComplete(delegate {

                if (count > 1)
                {
                    count -= 1;
                    tvCount.text = "" + count;
                }
                else
                {
                    tvCount.gameObject.SetActive(false);
                    objComplete.SetActive(true);
                    isComplete = true;
                }
                GamePlayController.Instance.gameScene.targetController.HanldCheckWin();

            });
        });
      
     
     
    }
}
