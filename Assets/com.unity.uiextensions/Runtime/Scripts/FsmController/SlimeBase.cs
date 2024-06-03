using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum SlimeType
{
    RED,
    BLUE,
    IRON,
    VIOLET,
    GHOST,
    ICE,
    FLASH
}
public class SlimeBase : BarrierBase
{
    public float Speed;
    public SlimeType slimeType;
    public SlimeFSMController fSMController;
    public Animator animator;
    public CircleCollider2D collider2D;

    public override void Init()
    {
        fSMController.Init(this);
    }

    public override void TakeDame()
    {
        if (!wasTakeDame)
        {
            wasTakeDame = true;
            Hp -= 1;
            StartCoroutine(HandleCountDown());
            if (fSMController.currentState.state != StateType.Hide)
            {
           
                spriteRenderer.DOFade(0.5f, 0.5f).OnComplete(delegate {
                    spriteRenderer.DOFade(1, 0.5f).OnComplete(delegate {
                        spriteRenderer.DOFade(0.5f, 0.3f).OnComplete(delegate {
                            spriteRenderer.DOFade(1, 0.3f).OnComplete(delegate {

                            });
                        });
                    });
                });
            }
         
           
           
        }
   
    }
    public void HandleHide(bool onColider)
    {
        if(onColider)
        {
            collider2D.enabled = true;
        }
        else
        {
            collider2D.enabled = false;
        }
    }
    public IEnumerator HandleCountDown()
    {
        yield return new WaitForSeconds(1.6f);
        wasTakeDame = false;
    }
    public void HandlePause()
    {
        this.transform.DOPause();
        StartCoroutine(HandleFree());
    }
    public IEnumerator HandleFree()
    {
        yield return new WaitForSeconds(5);
        this.transform.DOPlay();
    }



}
