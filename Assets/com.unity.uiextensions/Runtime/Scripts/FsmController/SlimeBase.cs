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
    public AudioSource takeDame;
    public AudioClip takeDameSFX;
    public HeartBarSlime heartBarSlime;
   
    public override void Init()
    {
        fSMController.Init(this);
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.FREEZE , HandlePause);
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.PAUSE, HandlePauseGame);
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.STOPPAUSE, HandleStopPauseGame);
        heartBarSlime.Init();
    }

    public override void TakeDame()
    {
        if (!wasTakeDame && GamePlayController.Instance.stateGame == StateGame.Playing)
        {
            wasTakeDame = true;
            StartCoroutine(Helper.HandleActionPlayAndWait( animator, "Hit", delegate { animator.Play("Move"); }));
          
            Hp -= 1;
            GamePlayController.Instance.playerContain.tutorial_BoomInput.NextTut();
            heartBarSlime.HandleSupTrackHeart();
            if (GameController.Instance.useProfile.OnSound)
            {
                takeDame.PlayOneShot(takeDameSFX);
            }
            
          
            if(Hp > 0)
            {
                StartCoroutine(HandleCountDown());
                GamePlayController.Instance.HandleCheckLose();
            } 
            else
            {
                collider2D.enabled = false;
            }
            
              if(fSMController.currentState != null)
            {
                if (fSMController.currentState.state != StateType.Hide)
                {


                    spriteRenderer.DOFade(0.5f, 0.5f).OnComplete(delegate
                    {
                        spriteRenderer.DOFade(1, 0.5f).OnComplete(delegate
                        {
                            spriteRenderer.DOFade(0.5f, 0.3f).OnComplete(delegate
                            {
                                spriteRenderer.DOFade(1, 0.3f).OnComplete(delegate
                                {

                                    //GamePlayController.Instance.HandleCheckLose();
                                });
                            });
                        });
                    });
                }

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
    public void HandlePause(object param)
    {
        this.transform.DOPause();
        if(fSMController.currentState.GetComponent<MoveState>() != null)
        {
            if (fSMController.currentState.GetComponent<MoveState>().isIdle)
            {
                fSMController.currentState.GetComponent<MoveState>().StopAllCoroutines();
            }
        }    
        StartCoroutine(HandleFree());
    }
    public IEnumerator HandleFree()
    {
        yield return new WaitForSeconds(5);
        this.transform.DOPlay();
        if (fSMController.currentState.GetComponent<MoveState>() != null)
        {
            if (fSMController.currentState.GetComponent<MoveState>().isIdle)
            {
                fSMController.currentState.GetComponent<MoveState>().HandleMove();
            }    
  
        }
    }
    public void HandlePauseGame(object param)
    {
        this.transform.DOPause();
       
    }
    public void HandleStopPauseGame(object param)
    {
        this.transform.DOPlay();

    }
    public void OnDestroy()
    {
         fSMController.idleState.StopAllCoroutines();
        fSMController.moveState.StopAllCoroutines();
        fSMController.hideState.StopAllCoroutines(); 
        fSMController.dieState.StopAllCoroutines();
        fSMController.moveState.DOKill();
        this.transform.DOKill();
        spriteRenderer.DOKill();
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.FREEZE, HandlePause);
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.PAUSE, HandlePauseGame);
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.STOPPAUSE, HandleStopPauseGame);
    }



}
