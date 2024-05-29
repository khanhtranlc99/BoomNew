using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    public void Init()
    {


    }    

    public void MoveCame(Vector3 post, Action callBack)
    {
        camera.transform.DOMove(new Vector3(post.x, post.y, -10),0.75f).OnComplete(delegate {
            if(callBack != null)
            {
                callBack?.Invoke();
            }        
        });
    }
    public void ZoomCame(float param, Action callBack)
    {
        if(param == camera.orthographicSize )
        {
            if (callBack != null)
            {
                callBack?.Invoke();
            }
            return;
        }    
        camera.DOOrthoSize(param +1.5f, 0.75f).OnComplete(delegate {
            if (callBack != null)
            {
                callBack?.Invoke();
            }
        }).SetEase(Ease.OutBack);
    }

}
