using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bloomsom : BarrierBase
{
    int random = 0;
    public List< SlimeBase> slimeBase = new List<SlimeBase>();
    public List<Lear> lsLear;
    public override void Init()
    {
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.FREEZE, HandlePause);
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.PAUSE, HandlePauseGame);
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.STOPPAUSE, HandleStopPauseGame);
    }
    private void Update()
    {
        if(gridBase.barrierBase != this)
        {
            Destroy(this.gameObject);
        }    
    }
    public override void TakeDame()
    {
        if (!wasTakeDame)
        {
            wasTakeDame = true;
            Hp -= 1;
            transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate { wasTakeDame = false; });
            if (Hp <= 0)
            {
              
                GameController.Instance.questController.HandleCheckCompleteQuest(questTargetType);
                gridBase.barrierBase = null;
                transform.DOKill();
                for (int i = 0; i < lsLear.Count; i++)
                {
                    int index = i;
                    lsLear[index].transform.DOKill();
                }
                transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate {
                    spriteRenderer.DOFade(0, 0.3f).OnComplete(delegate {


                      

                        Destroy(this.gameObject); 
                    
                    
                    });
                });

            }
        }
    }

    public void HandleSlimeHide()
    {
       
        random = Random.Range(0,2);
        barrierType = BarrierType.Block;
        for(int i = 0; i < lsLear.Count; i ++)
        {
            int index = i;
            lsLear[index].HandleMove(delegate { 
            
                if(index >= lsLear.Count -1)
                {
                    HandleSlimeOut();
                }
            
            });
 
        }
        //transform.DOShakePosition(1, 0.1f, 1, 1).SetDelay(0.5f).OnComplete(delegate
        //{
        //    //if(random == 1)
        //    //{
        //        HandleSlimeOut();
        //    //}
        //    //else
        //    //{
        //    //    HandleSlimeHide();
        //    //}
          
        //});
     
    }
    private void HandleSlimeOut()
    {
      
        barrierType = BarrierType.ComeThrough;
        for(int i = slimeBase.Count - 1; i >=  0; i --)
        {
            if (slimeBase[i] != null)
            {
                slimeBase[i].fSMController.ChangeState(StateType.Move);
                //Debug.LogError("StateType.Move");
                slimeBase.Remove(slimeBase[i]);
            }
        }
     
   
       
    }
    public void HandlePause(object param)
    {
        this.transform.DOPause();
        for (int i = 0; i < lsLear.Count; i++)
        {
            int index = i;
            lsLear[index].transform.DOPause();
        }
        StartCoroutine(HandleFree());
    }
    public IEnumerator HandleFree()
    {
        yield return new WaitForSeconds(5);
        for (int i = 0; i < lsLear.Count; i++)
        {
            int index = i;
            lsLear[index].transform.DOPlay();
        }
    }
    public void HandlePauseGame(object param)
    {
        try
        {
            this.transform.DOPause();
            for (int i = 0; i < lsLear.Count; i++)
            {
                int index = i;
                lsLear[index].transform.DOPause();
            }
        }
        catch
        {

        }
     
    }
    public void HandleStopPauseGame(object param)
    {
        this.transform.DOPlay();
        for (int i = 0; i < lsLear.Count; i++)
        {
            int index = i;
            lsLear[index].transform.DOPlay();
        }

    }
    public void OnDestroy()
    {
        this.transform.DOKill();
        for (int i = 0; i < lsLear.Count; i++)
        {
            int index = i;
            lsLear[index].transform.DOKill();
        }
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.FREEZE, HandlePause);
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.PAUSE, HandlePauseGame);
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.STOPPAUSE, HandleStopPauseGame);
    }
}

