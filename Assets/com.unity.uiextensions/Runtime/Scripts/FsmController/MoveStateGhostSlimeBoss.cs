using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveStateGhostSlimeBoss : SlimeStateBase
{
    public GridBase oldGrid;
    public GridBase nextGrid;
    public GridBase tempGrid;
    public List<int> idOld;
    public override void Init(SlimeBase slimeBase)
    {
        idOld = new List<int>();
        data = slimeBase;
    }

    public override void StartState()
    {

        data.spriteRenderer.DOFade(1, data.Speed);
        oldGrid = data.gridBase;
        idOld.Add(oldGrid.id);
        data.gridBase.barrierBase = null;
        HandleMove();
    }

    public override void UpdateState()
    {
        if (data.Hp <= 0)
        {
            data.fSMController.ChangeState(StateType.Die);
        }
    }
    public override void EndState()
    {
        this.transform.DOKill();
    }
    int coutLoop = 0;
    int ran;
    bool wasInvisible = false;
    int coutStep = 0;
    private void HandleMove()
    {

        coutStep += 1;
        if(coutStep == 2 && !wasInvisible)
        {
            data.spriteRenderer.DOColor(new Color32(0, 0, 0, 0), 0.4f);
            wasInvisible = true;
            coutStep = 0;
        }
        if (coutStep == 7 && wasInvisible)
        {
            wasInvisible = false;
            data.spriteRenderer.DOColor(new Color32(255, 255, 255, 255), 0.4f);
            coutStep = 0;
        }

 
       
        if (nextGrid == null)
        {
            tempGrid = oldGrid.GetNextGridForGhostSlime();
            nextGrid = tempGrid;
            if (nextGrid.transform.position.x < this.transform.position.x)
            {
                this.transform.localScale = new Vector3(2, 2, 2);
                data.heartBarSlime.transform.localScale = new Vector3(1, 1, 1);
            }
            if (nextGrid.transform.position.x > this.transform.position.x)
            {
                this.transform.localScale = new Vector3(-2, 2, 2);
                data.heartBarSlime.transform.localScale = new Vector3(-1, 1, 1);
            }

            oldGrid = nextGrid;


            this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate {

                idOld.Add(oldGrid.id);
                HandleMove();
            });
        }
        else
        {

            if (idOld.Count > 0)
            {
                tempGrid = oldGrid.GetNextGridForGhostSlime(idOld);
            }
            if (idOld.Count <= 0)
            {
                tempGrid = oldGrid.GetNextGridForGhostSlime();


            }
            if (tempGrid != null)
            {

                nextGrid = tempGrid;
                if (nextGrid.transform.position.x < this.transform.position.x)
                {
                    this.transform.localScale = new Vector3(2, 2, 2);
                    data.heartBarSlime.transform.localScale = new Vector3(1, 1, 1);
                }
                if (nextGrid.transform.position.x > this.transform.position.x)
                {
                    this.transform.localScale = new Vector3(-2, 2, 2);
                    data.heartBarSlime.transform.localScale = new Vector3(-1, 1, 1);
                }

                oldGrid = nextGrid;
                idOld.Add(oldGrid.id);
                this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate {
                    HandleMove();
                });
            }
            else
            {

                try
                {
                    idOld.Remove(idOld[idOld.Count - 2]);
                }
                catch
                {
                    idOld.Remove(idOld[idOld.Count - 1]);
                }
                coutLoop += 1;
                if (coutLoop > 3)
                {

                    idOld.Clear();
                    coutLoop = 0;
                }
                HandleMove();
            }
        }
    }
}