using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WoodenBox : BarrierBase
{
    public List<GiftInGame> lsGiftInGames;
    public ItemInGame itemInGame;
    public float randomGift;
    public GiftInGame currentGift;
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
            transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate { wasTakeDame = false; });
            if (Hp <= 0)
            {
                gridBase.barrierBase = null;
                transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate {
                    spriteRenderer.DOFade(0, 0.3f).OnComplete(delegate {
                        wasTakeDame = true;
                        transform.DOKill();
                        spriteRenderer.DOKill();


                        HandleDestroy();
                    });
                });

            }
        }



    }
    private void HandleDestroy()
    {
        randomGift = Random.RandomRange(0,100);
        currentGift = new GiftInGame();
        foreach (var item in lsGiftInGames)
        {
            if(randomGift >= item.percentDown && randomGift < item.percentUp)
            {
                currentGift = item;
            }
        }
        var temp = SimplePool2.Spawn(itemInGame  );
        temp.transform.parent =  GamePlayController.Instance.gameScene.canvas.transform;
        temp.transform.position = this.transform.position;
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.Init(currentGift, gridBase.GetRandomGrid.transform);
        //  temp.HandleJump(gridBase.GetRandomGrid.transform.position, null);


        GameController.Instance.questController.HandleCheckCompleteQuest(questTargetType);

        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        transform.DOKill();
        spriteRenderer.DOKill();
    }
}
[System.Serializable]
public class GiftInGame
{
    public Sprite sprite;
    public GiftType giftType;
    public float percentUp;
    public float percentDown;
    public int count;
 
}