using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IceSkillSlime : MonoBehaviour
{
    public GameObject vfxIce;
    public void HandleIceSkill()
    {
        var temp = new List<GridBase>();
        foreach (var item in GamePlayController.Instance.playerContain.levelData.gridBasesId)
        {
            if (item.barrierBase == null && !item.isFree)
            {
                temp.Add(item);
            }
        }
        temp.Shuffle();
        if (temp.Count >= 3)
        {



            var red_1 = SimplePool2.Spawn(vfxIce );
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(temp[0].transform.position, 0.2f, 1, 0.4f).OnComplete(delegate { temp[0].HandleFreeGrid(); SimplePool2.Despawn(red_1.gameObject);  });

            var red_2 = SimplePool2.Spawn(vfxIce );
            red_2.transform.position = this.transform.position;
            red_2.transform.DOJump(temp[1].transform.position, 0.2f, 1, 0.4f).OnComplete(delegate { temp[1].HandleFreeGrid(); SimplePool2.Despawn(red_2.gameObject); });

            var red_3 = SimplePool2.Spawn(vfxIce );
            red_3.transform.position = this.transform.position;
            red_3.transform.DOJump(temp[2].transform.position, 0.2f, 1, 0.4f).OnComplete(delegate { temp[2].HandleFreeGrid(); SimplePool2.Despawn(red_3.gameObject); });
            return;
        }
        if (temp.Count == 2)
        {



            var red_1 = Instantiate(vfxIce, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(temp[0].transform.position, 0.2f, 1, 0.4f).OnComplete(delegate { temp[0].HandleFreeGrid(); SimplePool2.Despawn(red_1.gameObject); });

            var red_2 = Instantiate(vfxIce, GamePlayController.Instance.playerContain.levelData.transform);
            red_2.transform.position = this.transform.position;
            red_2.transform.DOJump(temp[1].transform.position, 0.2f, 1, 0.4f).OnComplete(delegate { temp[1].HandleFreeGrid(); SimplePool2.Despawn(red_2.gameObject); });

           
            return;
        }
        if (temp.Count == 1)
        {



            var red_1 = Instantiate(vfxIce, GamePlayController.Instance.playerContain.levelData.transform);
            red_1.transform.position = this.transform.position;
            red_1.transform.DOJump(temp[0].transform.position, 0.2f, 1, 0.4f).OnComplete(delegate { temp[0].HandleFreeGrid(); SimplePool2.Despawn(red_1.gameObject); });

   


            return;
        }

    }    
}
