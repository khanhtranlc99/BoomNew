using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class ItemInGame : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void Init(Sprite param)
    {
        spriteRenderer.sprite = param;

    }
    public void HandleJump(Vector3 paramPost, Action callBack)
    {
        this.transform.DOJump(paramPost, 1,1,0.5f).SetEase(Ease.InOutBack).OnComplete(delegate {
         
            if(callBack != null)
            {
                callBack?.Invoke();
            }
        
        });
    }

    public void HandleMove(Vector3 paramPost)
    {
        this.transform.DOMove(paramPost, 0.5f).SetEase(Ease.InOutBack);
    }
}
