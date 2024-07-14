using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Gks;
public class MoveState : SlimeStateBase
{
    public GridBase oldGrid;
    public GridBase nextGrid;
    public GridBase tempGrid;
    public List<int> idOld;
    public bool isIdle;
    public override void Init(SlimeBase slimeBase)
    {
        idOld = new List<int>();
        data = slimeBase;
        isIdle = false;
    }

    public override void StartState()
    {
        
        data.spriteRenderer.DOFade(1, data.Speed);
        oldGrid = data.gridBase;
        idOld.Add(oldGrid.id);
        HandleMove();
    }

    public override void UpdateState()
    {
        if(data.Hp <= 0)
        {
            data.fSMController.ChangeState(StateType.Die);
        }
    }
   
    public override void EndState()
    {
        this.transform.DOKill();
        StopAllCoroutines();
    }
    int coutLoop = 0;
    public int ran;
    public void HandleMove()
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
                isIdle = true;
                StartCoroutine(Helper.HandleActionPlayAndWait(data.animator, "Idle", delegate { HandleMove(); }));
                return;
            }  
             
              
         
        }    
        else
        {
            if(!data.wasTakeDame)
            {
                isIdle = false;
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
                        this.transform.localScale = new Vector3( 1, 1, 1);
                    }
                    if (nextGrid.transform.position.x > this.transform.position.x)
                    {
                        this.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    if (oldGrid.barrierBase == data)
                    {
                        oldGrid.barrierBase = null;
                    }                
                    oldGrid = nextGrid;

                    oldGrid.barrierBase = data;
                    this.transform.DOMove(nextGrid.transform.position,Random.RandomRange(data.Speed, data.Speed+0.2f)).OnComplete(delegate {
                     
                        idOld.Add(oldGrid.id);
                        HandleMove();
                    });
                }
                else
                {
                    //Debug.LogError("???");
                    if (tempGrid.barrierBase.barrierType == BarrierType.ComeThrough)
                    {

                        nextGrid = tempGrid;
                        if (nextGrid.transform.position.x < this.transform.position.x)
                        {
                            this.transform.localScale = new Vector3(1, 1, 1);
                        }
                        if (nextGrid.transform.position.x > this.transform.position.x)
                        {
                            this.transform.localScale = new Vector3(-1, 1, 1);
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

                        this.transform.DOMove(nextGrid.transform.position, Random.RandomRange(data.Speed, data.Speed + 0.2f)).OnComplete(delegate
                        {

                            //Debug.LogError("ChangeState(StateType.Hide)");
                            data.fSMController.ChangeState(StateType.Hide);
                        });
                    }
                    if (tempGrid.barrierBase.barrierType == BarrierType.Boom  )
                    {
                        StartCoroutine(Helper.StartAction(delegate
                        {

                            //Debug.LogError("tempGrid.barrierBase == null");
                            nextGrid = tempGrid;
                            if (nextGrid.transform.position.x < this.transform.position.x)
                            {
                                this.transform.localScale = new Vector3(1, 1, 1);
                            }
                            if (nextGrid.transform.position.x > this.transform.position.x)
                            {
                                this.transform.localScale = new Vector3(-1, 1, 1);
                            }

                            if (oldGrid.barrierBase == data)
                            {
                                oldGrid.barrierBase = null;
                            }
                            oldGrid = nextGrid;
                            oldGrid.barrierBase = data;
                            idOld.Add(oldGrid.id);
                            this.transform.DOMove(nextGrid.transform.position, Random.RandomRange(data.Speed, data.Speed + 0.2f)).OnComplete(delegate
                            {
                                HandleMove();
                            });


                        }, () => FindNextMove));
                      
                    }
                    if(tempGrid != null)
                    {
                      if(tempGrid.barrierBase != null)
                        {
                            if (tempGrid.barrierBase.barrierType == BarrierType.Slime)
                            {
                                try
                                {
                                    if (tempGrid.barrierBase.barrierType == BarrierType.Slime)
                                    {

                                        StartCoroutine(Helper.StartAction(delegate
                                        {

                                            //Debug.LogError("tempGrid.barrierBase == null");
                                            nextGrid = tempGrid;
                                            if (nextGrid.transform.position.x < this.transform.position.x)
                                            {
                                                this.transform.localScale = new Vector3(1, 1, 1);
                                            }
                                            if (nextGrid.transform.position.x > this.transform.position.x)
                                            {
                                                this.transform.localScale = new Vector3(-1, 1, 1);
                                            }
                                            if (oldGrid.barrierBase == data)
                                            {
                                                oldGrid.barrierBase = null;
                                            }
                                            oldGrid = nextGrid;
                                            oldGrid.barrierBase = data;
                                            idOld.Add(oldGrid.id);
                                            this.transform.DOMove(nextGrid.transform.position, Random.RandomRange(data.Speed, data.Speed + 0.2f)).OnComplete(delegate
                                            {
                                                HandleMove();
                                            });


                                        }, () => FindNextMove));

                                    }

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
         
            if(idOld.Count > 0)
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
                        this.transform.localScale = new Vector3(1, 1, 1);
                    }
                    if (nextGrid.transform.position.x > this.transform.position.x)
                    {
                        this.transform.localScale = new Vector3(-1, 1, 1);
                    }

                    if (oldGrid.barrierBase == data  )
                    {
                        oldGrid.barrierBase = null;
                    }
                    oldGrid = nextGrid;
                    oldGrid.barrierBase = data;
                    idOld.Add(oldGrid.id);
                    
                    this.transform.DOMove(nextGrid.transform.position, data.Speed).OnComplete(delegate {

                   
                        HandleMove();
                    });
                }
                else
                {
                    if(tempGrid.barrierBase.barrierType == BarrierType.ComeThrough)
                    {
                     
                        nextGrid = tempGrid;
                        if (nextGrid.transform.position.x < this.transform.position.x)
                        {
                            this.transform.localScale = new Vector3(1, 1, 1);
                        }
                        if (nextGrid.transform.position.x > this.transform.position.x)
                        {
                            this.transform.localScale = new Vector3(-1, 1, 1);
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
                      
                        this.transform.DOMove(nextGrid.transform.position, Random.RandomRange(data.Speed, data.Speed + 0.2f)).OnComplete(delegate
                        {
              
                            //Debug.LogError("ChangeState(StateType.Hide)");
                            data.fSMController.ChangeState(StateType.Hide);
                        });
                    }
                    if (tempGrid.barrierBase.barrierType == BarrierType.Boom )
                    {
                      
                        StartCoroutine(Helper.StartAction(delegate
                        {

                            //Debug.LogError("tempGrid.barrierBase == null");
                            nextGrid = tempGrid;
                            if (nextGrid.transform.position.x < this.transform.position.x)
                            {
                                this.transform.localScale = new Vector3(1, 1, 1);
                            }
                            if (nextGrid.transform.position.x > this.transform.position.x)
                            {
                                this.transform.localScale = new Vector3(-1, 1, 1);
                            }

                            if (oldGrid.barrierBase == data)
                            {
                                oldGrid.barrierBase = null;
                            }
                            oldGrid = nextGrid;
                            oldGrid.barrierBase = data;
                            idOld.Add(oldGrid.id);

                       

                            this.transform.DOMove(nextGrid.transform.position, Random.RandomRange(data.Speed, data.Speed + 0.2f)).OnComplete(delegate
                            {
                            

                                HandleMove();
                            });


                        }, () => FindNextMove));
                    }
             
                    if(tempGrid != null)
                    {
                        if (tempGrid.barrierBase != null)
                        {
                            if (tempGrid.barrierBase.barrierType == BarrierType.Slime)
                            {

                                try
                                {
                                    if (tempGrid.barrierBase.barrierType == BarrierType.Slime)
                                    {
                                        StartCoroutine(Helper.StartAction(delegate
                                        {

                                            //Debug.LogError("tempGrid.barrierBase == null");
                                            nextGrid = tempGrid;
                                            if (nextGrid.transform.position.x < this.transform.position.x)
                                            {
                                                this.transform.localScale = new Vector3(1, 1, 1);
                                            }
                                            if (nextGrid.transform.position.x > this.transform.position.x)
                                            {
                                                this.transform.localScale = new Vector3(-1, 1, 1);
                                            }

                                            if (oldGrid.barrierBase == data)
                                            {
                                                oldGrid.barrierBase = null;
                                            }
                                            oldGrid = nextGrid;
                                            oldGrid.barrierBase = data;
                                            idOld.Add(oldGrid.id);
                                            this.transform.DOMove(nextGrid.transform.position, Random.RandomRange(data.Speed, data.Speed + 0.2f)).OnComplete(delegate
                                            {
                                                HandleMove();
                                            });


                                        }, () => FindNextMove));
                                    }
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

