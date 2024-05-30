using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DieStateBlueSlime : SlimeStateBase
{
    public SlimeBase redSlime;
    public override void EndState()
    {
  
    }

    public override void Init(SlimeBase slimeBase)
    {
        data = slimeBase;
    }

    public override void StartState()
    {
        data.animator.Play("Die");
    }

    public override void UpdateState()
    {
     
    }
    public void HandleActionDie()
    {

      
        var temp = new List<GridBase>();
        foreach(var item in data.gridBase.lsGridBase)
        {
            if(item.barrierBase == null)
            {
                temp.Add(item);
            }
        }
        if(temp.Count >= 2)
        {
            data.gridBase.barrierBase = null;
            var red_1 = Instantiate(redSlime ,GamePlayController.Instance.playerContain.levelData.transform);
            red_1.gridBase = temp[0];
            red_1.transform.position = this.transform.position;         
            red_1.transform.DOJump(temp[0].transform.position, 0.2f,1,0.3f).OnComplete(delegate { red_1.Init();  });

            var red_2 = Instantiate(redSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_2.gridBase = temp[1];
            red_2.transform.position = this.transform.position;
            red_2.transform.DOJump(temp[1].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_2.Init();  });
        }
        if (temp.Count == 1)
        {
            var red_1 = Instantiate(redSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.gridBase = data.gridBase;
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(data.gridBase.transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_1.Init(); });


            var red_2 = Instantiate(redSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_2.gridBase = temp[0];
            red_2.transform.position = this.transform.position;
            red_2.transform.DOJump(temp[0].transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_2.Init(); });
        }
        if (temp.Count == 0)
        {
            var red_1 = Instantiate(redSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.gridBase = data.gridBase;
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(data.gridBase.transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_1.Init(); });


            var red_2 = Instantiate(redSlime, GamePlayController.Instance.playerContain.levelData.transform);
            red_2.gridBase = data.gridBase;
            red_2.transform.position = this.transform.position;
            red_2.transform.DOJump(data.gridBase.transform.position, 0.2f, 1, 0.3f).OnComplete(delegate { red_2.Init(); });
        }



        Destroy(data.gameObject);
    }

}
