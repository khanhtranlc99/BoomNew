using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bloomsom : BarrierBase
{
    int random = 0;
    public SlimeBase slimeBase;
    public override void Init()
    {

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
            if(random == 1)
            {
                HandleSlimeOut();
            }
            else
            {
                HandleSlimeHide();
            }
          
        });
     
    }
    private void HandleSlimeOut()
    {
      
        barrierType = BarrierType.ComeThrough;
        if(slimeBase != null)
        {
            slimeBase.fSMController.ChangeState(StateType.Move);
            Debug.LogError("StateType.Move");
            slimeBase = null;
        }
       
    }
   
}

