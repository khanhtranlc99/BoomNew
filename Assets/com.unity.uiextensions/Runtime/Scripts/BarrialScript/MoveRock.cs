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
            if (collision.gameObject.GetComponent<SlimeBase>() != null)
            {
                if(collision.gameObject.GetComponent<SlimeBase>().slimeType == SlimeType.ICE  )
                {
                    collision.gameObject.GetComponent<SlimeBase>().fSMController.Stop();
                    moveGrid.barrierBase = null;
                    collision.gameObject.GetComponent<MoveStateIceSlime>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveStateIceSlime>().tempGrid = null;
                    collision.gameObject.GetComponent<MoveStateIceSlime>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveStateIceSlime>().idOld.Add(moveGrid.id);
                    moveGrid.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = moveGrid;
                    collision.gameObject.transform.DOMove(moveGrid.transform.position, 0.5f).OnComplete(delegate {
                        collision.gameObject.GetComponent<SlimeBase>().fSMController.ChangeState(StateType.Move);
                    });

                }
                else if (collision.gameObject.GetComponent<SlimeBase>().slimeType == SlimeType.GHOST)
                {
                    collision.gameObject.GetComponent<SlimeBase>().fSMController.Stop();
                    moveGrid.barrierBase = null;
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().tempGrid = null;
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveStateGhostSlime>().idOld.Add(moveGrid.id);
                    moveGrid.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = moveGrid;
                    collision.gameObject.transform.DOMove(moveGrid.transform.position, 0.5f).OnComplete(delegate {
                        collision.gameObject.GetComponent<SlimeBase>().fSMController.ChangeState(StateType.Move);
                    });
                }
                else 
                {
                    collision.gameObject.GetComponent<SlimeBase>().fSMController.Stop();
                    moveGrid.barrierBase = null;
                    collision.gameObject.GetComponent<MoveState>().nextGrid = null;
                    collision.gameObject.GetComponent<MoveState>().tempGrid = null;
                    collision.gameObject.GetComponent<MoveState>().idOld.Clear();
                    collision.gameObject.GetComponent<MoveState>().idOld.Add(moveGrid.id);
                    moveGrid.barrierBase = collision.gameObject.GetComponent<BarrierBase>();
                    collision.gameObject.GetComponent<SlimeBase>().gridBase = moveGrid;
                    collision.gameObject.transform.DOMove(moveGrid.transform.position, 0.5f).OnComplete(delegate {
                        collision.gameObject.GetComponent<SlimeBase>().fSMController.ChangeState(StateType.Move);
                    });
                }

           
            }
        }
        if (collision.gameObject.tag == "Boom")
        {

           

        }
    }

    [Button]
    public void HandleSetUp()
    {
        moveGrid = gridBase.GetGrid(moveBlockType);
        gridBase.barrierBase = null;
        gridBase = null;



    }

}