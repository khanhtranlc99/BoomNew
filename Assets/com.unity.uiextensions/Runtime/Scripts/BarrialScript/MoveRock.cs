using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public enum MoveBlockType
{
    Up,
    Down,
    Left,
    Right,
}


public class MoveRock : BarrierBase
{
    public GridBase moveGrid;
    public MoveBlockType moveBlockType;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Barrial")
        {
            if (moveGrid.barrierBase != null)
            {
                if (collision.gameObject.GetComponent<SlimeBase>().slimeType == SlimeType.ICE)
                {
                    collision.gameObject.GetComponent<MoveStateIceSlime>().idOld.Add(gridBase.id);
                }
                else if (collision.gameObject.GetComponent<SlimeBase>().slimeType == SlimeType.GHOST)
                {
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().idOld.Add(gridBase.id);
                }
                else
                {
                    collision.gameObject.GetComponent<MoveState>().idOld.Add(gridBase.id);
                }
               return;
            }
           
                if (collision.gameObject.GetComponent<SlimeBase>() != null)
            {
             
                if (collision.gameObject.GetComponent<SlimeBase>().slimeType == SlimeType.ICE  )
                {
                    collision.gameObject.GetComponent<SlimeBase>().fSMController.Stop();
                    moveGrid.barrierBase = null;
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = null;
                    if (collision.gameObject.GetComponent<MoveStateIceSlime>().oldGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveStateIceSlime>().oldGrid.barrierBase = null;
                    }
                    if (collision.gameObject.GetComponent<MoveStateIceSlime>().nextGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveStateIceSlime>().nextGrid.barrierBase = null;
                    }
                    if (collision.gameObject.GetComponent<MoveStateIceSlime>().tempGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveStateIceSlime>().tempGrid.barrierBase = null;
                    }
                    collision.gameObject.GetComponent<MoveStateIceSlime>().oldGrid = null;
                    collision.gameObject.GetComponent<MoveStateIceSlime>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveStateIceSlime>().tempGrid = null;
                    //collision.gameObject.GetComponent<MoveStateIceSlime>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveStateIceSlime>().idOld.Add(moveGrid.id);
                    collision.gameObject.GetComponent<MoveStateIceSlime>().idOld.Add(gridBase.id);
                    moveGrid.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = moveGrid;
                    gridBase.barrierBase = null;
                    collision.gameObject.transform.DOMove(moveGrid.transform.position, 0.5f).OnComplete(delegate
                    {
                        collision.gameObject.GetComponent<SlimeBase>().fSMController.ChangeState(StateType.Move);
                    });
                }
                else if (collision.gameObject.GetComponent<SlimeBase>().slimeType == SlimeType.GHOST)
                {
                    collision.gameObject.GetComponent<SlimeBase>().fSMController.Stop();
                    moveGrid.barrierBase = null;
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = null;
                    if (collision.gameObject.GetComponent<MoveStateGhostSlime>().oldGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveStateGhostSlime>().oldGrid.barrierBase = null;
                    }
                    if (collision.gameObject.GetComponent<MoveStateGhostSlime>().nextGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveStateGhostSlime>().nextGrid.barrierBase = null;
                    }
                    if (collision.gameObject.GetComponent<MoveStateGhostSlime>().tempGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveStateGhostSlime>().tempGrid.barrierBase = null;
                    }
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().oldGrid = null;
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().tempGrid = null;
                    //collision.gameObject.GetComponent<MoveStateGhostSlime>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().idOld.Add(moveGrid.id);
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().idOld.Add(gridBase.id);
                    moveGrid.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = moveGrid;
                    
                    gridBase.barrierBase = null;
                    collision.gameObject.transform.DOMove(moveGrid.transform.position, 0.5f).OnComplete(delegate
                    {
                        collision.gameObject.GetComponent<SlimeBase>().fSMController.ChangeState(StateType.Move);
                    });
                }
                else 
                {
                    collision.gameObject.GetComponent<SlimeBase>().fSMController.Stop();
                    moveGrid.barrierBase = null;
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = null;
                    if (collision.gameObject.GetComponent<MoveState>().oldGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveState>().oldGrid.barrierBase = null;
                    }
                    if (collision.gameObject.GetComponent<MoveState>().nextGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveState>().nextGrid.barrierBase = null;
                    }
                    if (collision.gameObject.GetComponent<MoveState>().tempGrid != null)
                    {
                        collision.gameObject.GetComponent<MoveState>().tempGrid.barrierBase = null;
                    }           
                    collision.gameObject.GetComponent<MoveState>().oldGrid = null;
                    collision.gameObject.GetComponent<MoveState>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveState>().tempGrid = null;
                    //collision.gameObject.GetComponent<MoveState>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveState>().idOld.Add(moveGrid.id);
                    collision.gameObject.GetComponent<MoveState>().idOld.Add(gridBase.id);
                    moveGrid.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = moveGrid;
                    gridBase.barrierBase = null;
                    collision.gameObject.transform.DOMove(moveGrid.transform.position, 0.5f).OnComplete(delegate
                    {
                        collision.gameObject.GetComponent<SlimeBase>().fSMController.ChangeState(StateType.Move);
                    });
                }

           
            }
        }
        if (collision.gameObject.tag == "Boom")
        {

           

            if (moveGrid.barrierBase != null)
            {
                StartCoroutine(Helper.StartAction(delegate
                {
                    if (collision.gameObject.GetComponent<BarrierBase>() != null && collision.gameObject.activeSelf)
                    {
                        collision.gameObject.GetComponent<BarrierBase>().gridBase.barrierBase = null;
                        moveGrid.barrierBase = null;
                        moveGrid.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                        collision.gameObject.GetComponent<BarrierBase>().gridBase = moveGrid;
                        collision.gameObject.transform.DOMove(moveGrid.transform.position, 0.5f).OnComplete(delegate
                        {

                        });
                    }


                }, () => moveGrid.barrierBase == null));
                //Debug.LogError("moveGrid.barrierBase != null");
            }
            else
            {
                collision.gameObject.GetComponent<BarrierBase>().gridBase.barrierBase = null;
                moveGrid.barrierBase = null;
                moveGrid.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                collision.gameObject.GetComponent<BarrierBase>().gridBase = moveGrid;
                collision.gameObject.transform.DOMove(moveGrid.transform.position, 0.5f).OnComplete(delegate
                {

                });
                //Debug.LogError("moveGrid.barrierBase != null");
            }

        }
    }

    [Button]
    public void HandleSetUp()
    {
        moveGrid = gridBase.GetGrid(moveBlockType);
        gridBase.barrierBase = null;
        if(gridBase != null)
        {
            Debug.LogError("!=null");
        }
        else
        {
            Debug.LogError("===null");
        }
        if (moveGrid.lsGridBase.Contains(gridBase))
        {
            moveGrid.lsGridBase.Remove(gridBase);
        }

        //gridB;ase = null



    }

}