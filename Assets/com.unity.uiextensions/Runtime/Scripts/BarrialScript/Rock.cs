using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rock : BarrierBase
{
    public Sprite spriteHit_1;
    public Sprite spriteHit_2;
    public ParticleSystem particleSystem;
    public override void Init()
    {

    }
    public override void TakeDame()
    {
        if (!wasTakeDame)
        {
            wasTakeDame = true;
            Hp -= 1;
            particleSystem.Play();
            if (Hp == 2)
            {
                spriteRenderer.sprite = spriteHit_1;
            }
            if (Hp == 1)
            {
                spriteRenderer.sprite = spriteHit_2;
            }
            transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate { wasTakeDame = false; });
            if (Hp <= 0)
            {
                gridBase.barrierBase = null;
                transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate {
                    spriteRenderer.DOFade(0, 0.3f).OnComplete(delegate {

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
    private void OnDestroy()
    {
        transform.DOKill();
        spriteRenderer.DOKill();
    }
}
