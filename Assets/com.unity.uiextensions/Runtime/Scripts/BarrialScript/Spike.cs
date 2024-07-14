using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class Spike : BarrierBase
{

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

                transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate
                {
                    spriteRenderer.DOFade(0, 0.3f).OnComplete(delegate
                    {
                        wasTakeDame = true;
                        transform.DOKill();
                        spriteRenderer.DOKill();
                        GameController.Instance.questController.HandleCheckCompleteQuest(questTargetType);
                        Destroy(this.gameObject);
                    });
                });

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrial")
        {
            collision.gameObject.GetComponent<BarrierBase>().TakeDame();
        }
         
    }
    [Button]
    public void HandleTools()
    {
        gridBase.barrierBase = null;
    }

    private void OnDestroy()
    {
        transform.DOKill();
        spriteRenderer.DOKill();
    }

}
