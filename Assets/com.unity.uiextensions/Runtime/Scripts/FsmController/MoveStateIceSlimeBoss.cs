using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveStateIceSlimeBoss : SlimeStateBase
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
        oldGrid.HandleFreeGrid();
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
    private void HandleMove()
    {
        //if (idOld.Count > 4)
        //{
        //    idOld.Remove(idOld[0]);
        //}
        ran = Random.Range(0, 2);
        if (ran == 0)
        {
            if (!data.wasTakeDame)
            {

                StartCoroutine(Helper.HandleActionPlayAndWait(data.animator, "Ice", delegate { HandleMove(); }));
                return;
            }
        }
        else
        {
            if (!data.wasTakeDame)
            {

                data.animator.Play("Move");
            }
        }
        if (nextGrid == null)
        {
          

            tempGrid = oldGrid.GetNextGrid();

            if (tempGrid != null)
            {
                if (tempGrid.barrierBase == null)
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
                    if (oldGrid.barrierBase == data)
                    {
                        oldGrid.barrierBase = null;
                    }
                    oldGrid = nextGrid;

                    oldGrid.barrierBase = data;
                    this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate {

                        idOld.Add(oldGrid.id);
                        oldGrid.HandleFreeGrid();
                        HandleMove();
                    });
                }
                else
                {
                    Debug.LogError("???");
                    if (tempGrid.barrierBase.barrierType == BarrierType.ComeThrough)
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
                        data.spriteRenderer.DOKill();
                        data.spriteRenderer.DOFade(0, data.Speed);
                        if (oldGrid.barrierBase == data)
                        {
                            oldGrid.barrierBase = null;
                        }

                        oldGrid = nextGrid;
                        //oldGrid.barrierBase = data;
                        data.gridBase = oldGrid;
                        idOld.Add(oldGrid.id);

                        this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate
                        {
                            oldGrid.HandleFreeGrid();
                            //Debug.LogError("ChangeState(StateType.Hide)");
                            data.fSMController.ChangeState(StateType.Hide);
                        });
                    }
                    if (tempGrid.barrierBase.barrierType == BarrierType.Boom)
                    {
                        StartCoroutine(Helper.StartAction(delegate
                        {

                            //Debug.LogError("tempGrid.barrierBase == null");
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

                            if (oldGrid.barrierBase == data)
                            {
                                oldGrid.barrierBase = null;
                            }
                            oldGrid = nextGrid;
                            oldGrid.barrierBase = data;
                            idOld.Add(oldGrid.id);
                            this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate
                            {
                                oldGrid.HandleFreeGrid();
                                HandleMove();
                            });


                        }, () => FindNextMove));

                    }
                    if (tempGrid != null)
                    {
                        if (tempGrid.barrierBase != null)
                        {
                            if (tempGrid.barrierBase.barrierType == BarrierType.Slime)
                            {
                                try
                                {
                                    StartCoroutine(Helper.StartAction(delegate
                                    {

                                        //Debug.LogError("tempGrid.barrierBase == null");
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

                                        if (oldGrid.barrierBase == data)
                                        {
                                            oldGrid.barrierBase = null;
                                        }
                                        oldGrid = nextGrid;
                                        oldGrid.barrierBase = data;
                                        idOld.Add(oldGrid.id);
                                        this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate
                                        {
                                            oldGrid.HandleFreeGrid();
                                            HandleMove();
                                        });


                                    }, () => FindNextMove));
                                }
                                catch
                                {
                                    this.transform.DOKill();
                                    HandleMove();
                                }


                            }
                        }

                    }


                }
            }
        }
        else
        {

            if (idOld.Count > 0)
            {
                tempGrid = oldGrid.GetNextGrid(idOld);
            }
            if (idOld.Count <= 0)
            {
                tempGrid = oldGrid.GetNextGrid();
                //Debug.LogError("GetNextGrid(111)");

            }
            if (tempGrid != null)
            {

                if (tempGrid.barrierBase == null)
                {
                    //Debug.LogError("tempGrid.barrierBase == null");
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

                    if (oldGrid.barrierBase == data)
                    {
                        oldGrid.barrierBase = null;
                    }
                    oldGrid = nextGrid;
                    oldGrid.barrierBase = data;
                    idOld.Add(oldGrid.id);

                    this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate {

                        oldGrid.HandleFreeGrid();
                        HandleMove();
                    });
                }
                else
                {
                    if (tempGrid.barrierBase.barrierType == BarrierType.ComeThrough)
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
                        data.spriteRenderer.DOKill();
                        data.spriteRenderer.DOFade(0, data.Speed);
                        if (oldGrid.barrierBase == data)
                        {
                            oldGrid.barrierBase = null;
                        }

                        oldGrid = nextGrid;
                        //oldGrid.barrierBase = data;
                        data.gridBase = oldGrid;
                        idOld.Add(oldGrid.id);

                        this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate
                        {
                            oldGrid.HandleFreeGrid();
                            //Debug.LogError("ChangeState(StateType.Hide)");
                            data.fSMController.ChangeState(StateType.Hide);
                        });
                    }
                    if (tempGrid.barrierBase.barrierType == BarrierType.Boom)
                    {

                        StartCoroutine(Helper.StartAction(delegate
                        {

                            //Debug.LogError("tempGrid.barrierBase == null");
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
                            if (oldGrid.barrierBase == data)
                            {
                                oldGrid.barrierBase = null;
                            }
                            oldGrid = nextGrid;
                            oldGrid.barrierBase = data;
                            idOld.Add(oldGrid.id);



                            this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate
                            {

                                oldGrid.HandleFreeGrid();
                                HandleMove();
                            });


                        }, () => FindNextMove));
                    }
                    if (tempGrid != null)
                    {
                        if (tempGrid.barrierBase != null)
                        {
                            if (tempGrid.barrierBase.barrierType == BarrierType.Slime)
                            {
                                try
                                {
                                    StartCoroutine(Helper.StartAction(delegate
                                    {

                                        //Debug.LogError("tempGrid.barrierBase == null");
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

                                        if (oldGrid.barrierBase == data)
                                        {
                                            oldGrid.barrierBase = null;
                                        }
                                        oldGrid = nextGrid;
                                        oldGrid.barrierBase = data;
                                        idOld.Add(oldGrid.id);
                                        this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate
                                        {
                                            oldGrid.HandleFreeGrid();
                                            HandleMove();
                                        });


                                    }, () => FindNextMove));
                                }
                                catch
                                {
                                    this.transform.DOKill();
                                    HandleMove();
                                }

                            }
                        }

                    }



                }


            }
            else
            {


                //Debug.LogError("tempGrid == null");
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
                    //Debug.LogError("Clear");
                    idOld.Clear();
                    coutLoop = 0;
                }
                HandleMove();

            }
        }
    }
    public void HandleClear()
    {



    }


    public bool FindNextMove
    {
        get
        {
            tempGrid = oldGrid.GetNextGrid(true);
            if (tempGrid != null)
            {
                return true;
            }
            return false;
        }

    }
}