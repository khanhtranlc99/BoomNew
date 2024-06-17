using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bloomsom : BarrierBase
{
    int random = 0;
    public List< SlimeBase> slimeBase = new List<SlimeBase>();
    public override void Init()
    {
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.FREEZE, HandlePause);
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
              
                gridBase.barrierBase = null;
                transform.DOKill();
                transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate {
                    spriteRenderer.DOFade(0, 0.3f).OnComplete(delegate {

                        //if(slimeBase != null)
                        //{
                        //    HandleSlimeOut();
                        //}
                        GameController.Instance.questController.HandleCheckCompleteQuest( questTargetType);
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
        transform.DOShakePosition(1, 0.1f, 1, 1).SetDelay(0.5f).OnComplete(delegate
        {
            //if(random == 1)
            //{
                HandleSlimeOut();
            //}
            //else
            //{
            //    HandleSlimeHide();
            //}
          
        });
     
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
        StartCoroutine(HandleFree());
    }
    public IEnumerator HandleFree()
    {
        yield return new WaitForSeconds(5);
        this.transform.DOPlay();
    }
    public void OnDestroy()
    {
        this.transform.DOKill();
      EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.FREEZE, HandlePause);
    }
}

