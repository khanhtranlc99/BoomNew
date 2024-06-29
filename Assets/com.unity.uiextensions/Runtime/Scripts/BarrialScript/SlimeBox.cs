using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SlimeBox : BarrierBase
{
    public Sprite spriteHit_1;
    public Sprite spriteHit_2;
    public List<SlimeBase> lsSlimeBases;
    private SlimeBase GetSlime
    {
        get
        {
            return lsSlimeBases[Random.RandomRange(0, lsSlimeBases.Count)];
        }
    }
 
    public override void Init()
    {

    }
    public override void TakeDame()
    {
        if (!wasTakeDame)
        {
            wasTakeDame = true;
            Hp -= 1;
            if(Hp == 2)
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
            
                transform.DOShakePosition(0.3f, 0.1f, 1, 1).OnComplete(delegate {
                    spriteRenderer.DOFade(0, 0.7f).OnComplete(delegate {

                        HandleSpawnSlime();
                        gridBase.barrierBase = null;
                        Destroy(this.gameObject);
                    });
                });

            }
        }
    }

    private void HandleSpawnSlime()
    {
        Debug.LogError("HandleSpawnSlime");
        var temp = new List<GridBase>();
        foreach (var item in  gridBase.lsGridBase)
        {
            if (item.barrierBase == null)
            {
                temp.Add(item);
            }
        }
        if (temp.Count >= 3)
        {
            gridBase.barrierBase = null;
            var red_1 = Instantiate(GetSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.gridBase = temp[0];
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(temp[0].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_1.Init(); });

            var red_2 = Instantiate(GetSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_2.gridBase = temp[1];
            red_2.transform.position = this.transform.position;
            red_2.transform.DOJump(temp[1].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_2.Init(); });

            var red_3 = Instantiate(GetSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_3.gridBase = temp[2];
            red_3.transform.position = this.transform.position;
            red_3.transform.DOJump(temp[2].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_3.Init(); });

            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_1);
            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_2);
            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_3);
        }
        if (temp.Count == 2)
        {
            gridBase.barrierBase = null;
            var red_1 = Instantiate(GetSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.gridBase = temp[0];
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(temp[0].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_1.Init(); });

            var red_2 = Instantiate(GetSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_2.gridBase = temp[1];
            red_2.transform.position = this.transform.position;
            red_2.transform.DOJump(temp[1].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_2.Init(); });
   
            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_1);
            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_2);
        }
        if (temp.Count == 1)
        {
            gridBase.barrierBase = null;
            var red_1 = Instantiate(GetSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.gridBase = temp[0];
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(temp[0].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_1.Init(); });
 
            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_1);
 
        }
        if (temp.Count == 0)
        {
            gridBase.barrierBase = null;
            var red_1 = Instantiate(GetSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.gridBase = this.gridBase;
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(this.transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_1.Init(); });

            GamePlayController.Instance.playerContain.levelData.lsSmiles.Add(red_1);

        }
    }    

 


}