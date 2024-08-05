using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class Lear : MonoBehaviour
{
    // Start is called before the first frame update


    public void HandleMove(Action callBack)
    {
        transform.DORotate(new Vector3(0,0, UnityEngine.Random.RandomRange(17,-17)), 0.3f).SetEase(Ease.Linear).OnComplete(delegate {
            transform.DORotate(new Vector3(0, 0, UnityEngine.Random.RandomRange(17, -17)), 0.3f).SetEase(Ease.Linear).OnComplete(delegate {
                transform.DORotate(new Vector3(0, 0, UnityEngine.Random.RandomRange(17, -17)), 0.3f).SetEase(Ease.Linear).OnComplete(delegate {


                    transform.DORotate(new Vector3(0, 0, 0), 0.3f).OnComplete(delegate { callBack?.Invoke(); });

                });



            });     
        });
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
