using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Chest : BarrierBase
{

    public List<GiftInGame> lsGiftInGames;
    public ItemInGame itemInGame;
    public float randomGift;
    public GiftInGame currentGift;
    public Sprite spriteHit_1;
    public Sprite spriteHit_2;
    public override void Init()
    {

    }
    public override void TakeDame()
    {
        if (!wasTakeDame)
        {
            wasTakeDame = true;
            Hp -= 1;
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



                        HandleDestroy();
                    });
                });

            }
        }



    }
    private void HandleDestroy()
    {
        randomGift = Random.RandomRange(0, 100);
        currentGift = new GiftInGame();
        foreach (var item in lsGiftInGames)
        {
            if (randomGift >= item.percentDown && randomGift < item.percentUp)
            {
                currentGift = item;
            }
        }
        var temp = SimplePool2.Spawn(itemInGame);
        temp.transform.parent = GamePlayController.Instance.gameScene.canvas.transform;
        temp.transform.position = this.transform.position;
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.Init(currentGift, gridBase.GetRandomGrid.transform);
        //  temp.HandleJump(gridBase.GetRandomGrid.transform.position, null);



        GameController.Instance.questController.HandleCheckCompleteQuest(questTargetType);
        Destroy(this.gameObject);
    }
}
