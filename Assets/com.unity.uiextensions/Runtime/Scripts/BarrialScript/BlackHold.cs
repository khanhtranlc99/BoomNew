using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class BlackHold : BarrierBase
{
    public BlackHold blackHold;
    public GridBase gridBlackHold;
    public List<SlimeBase> lsSlimeBases;
 
    public override void Init()
    {

    }
    public override void TakeDame()
    {
        //if (!wasTakeDame)
        //{
        //    wasTakeDame = true;
        //    Hp -= 1;
        //    if (blackHold != null)
        //    {
        //        blackHold.Off();
        //    }
        //    transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate { wasTakeDame = false; });
        //    if (Hp <= 0)
        //    {
              
        //        transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate {
        //            spriteRenderer.DOFade(0, 0.3f).OnComplete(delegate {
        //                if(blackHold!= null)
        //                {
        //                    blackHold.TakeDame();
        //                }
                      
        //                Destroy(this.gameObject);
        //            });
        //        });

        //    }
        //}
    }
    public void Off()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrial")
        {
            if(collision.gameObject.GetComponent<SlimeBase>() != null)
            {
                if(lsSlimeBases.Contains(collision.gameObject.GetComponent<SlimeBase>()))
                {
                    return;
                }
                if(blackHold.gridBlackHold.barrierBase != null)
                {
                    return;
                }    

                lsSlimeBases.Add(collision.gameObject.GetComponent<SlimeBase>());
                blackHold.lsSlimeBases.Add(collision.gameObject.GetComponent<SlimeBase>());
                collision.gameObject.GetComponent<SlimeBase>().fSMController.Stop();
                gridBlackHold.barrierBase = null;
                if (collision.gameObject.GetComponent<SlimeBase>().slimeType == SlimeType.ICE)
                {
                    collision.gameObject.GetComponent<MoveStateIceSlime>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveStateIceSlime>().tempGrid = null;
                    collision.gameObject.GetComponent<MoveStateIceSlime>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveStateIceSlime>().idOld.Add(blackHold.gridBlackHold.id);
                }
                else if(collision.gameObject.GetComponent<SlimeBase>().slimeType == SlimeType.GHOST)
                {
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().tempGrid = null;
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().idOld.Add(blackHold.gridBlackHold.id);
                }
                else
                {
                    collision.gameObject.GetComponent<MoveState>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveState>().tempGrid = null;
                    collision.gameObject.GetComponent<MoveState>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveState>().idOld.Add(blackHold.gridBlackHold.id);
                }
             
                blackHold.gridBlackHold.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                collision.gameObject.GetComponent<SlimeBase>().gridBase = blackHold.gridBlackHold;

                collision.gameObject.transform.DOScale(Vector3.zero, 0.5f).OnComplete(delegate {        
                    collision.gameObject.transform.position = blackHold.transform.position;
                    collision.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate {

                        collision.gameObject.GetComponent<SlimeBase>().fSMController.ChangeState(StateType.Move);
                    });

                });    
            }    
        }
        if (collision.gameObject.tag == "Boom")
        {
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;  
            gridBlackHold.barrierBase = null;
            blackHold.gridBlackHold.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
            collision.gameObject.GetComponent<BarrierBase>().gridBase = blackHold.gridBlackHold;

            collision.gameObject.transform.DOScale(Vector3.zero, 0.5f).OnComplete(delegate {
                collision.gameObject.transform.position = blackHold.transform.position;
                collision.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate {      
                });
            });

            if (collision.gameObject.GetComponent<Boom>() != null)
            {
                collision.gameObject.GetComponent<Boom>().Init();
            }
            if (collision.gameObject.GetComponent<FastBoom>() != null)
            {
                collision.gameObject.GetComponent<FastBoom>().Init();
            }
            if (collision.gameObject.GetComponent<TimeBoom>() != null)
            {
                collision.gameObject.GetComponent<TimeBoom>().Init();
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (lsSlimeBases.Contains(collision.gameObject.GetComponent<SlimeBase>()))
        {
            lsSlimeBases.Remove(collision.gameObject.GetComponent<SlimeBase>());
        }
    }

    [Button]
    private void HandleSetUp()
    {

        gridBlackHold = gridBase;
        gridBase.barrierBase = null;
        gridBase = null;
        

    }
}