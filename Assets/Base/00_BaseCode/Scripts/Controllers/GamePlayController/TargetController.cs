using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public List<SlimeTarget> lsSlimeTargets;
    public Transform tranformSlimeTarget;
    public Transform tranformSlimeTargetLarge;
    public List<SlimeTarget> lsCurrentSlimeTargets;

    public Transform tranformSlimePrepage;
    public SlimeTarget GetSlimeTargetPrefab(SlimeType param)
    {
        foreach(var item in lsSlimeTargets)
        {
            if(item.slimeType == param)
            {
                return item;
            }

        }
        return null;
    }

    public SlimeTarget GetSlimeTarget(SlimeType param)
    {
        foreach (var item in lsCurrentSlimeTargets)
        {
            if (item.slimeType == param)
            {
                return item;
            }

        }
        return null;
    }
    public void Init(LevelData levelData )
    {
        lsCurrentSlimeTargets = new List<SlimeTarget>();
        if (levelData.isSlimeLevel)
        {
            foreach (var item in levelData.conditionSlimes.lsDataSlime)
            {
                var temp = Instantiate(GetSlimeTargetPrefab(item.slimeType), tranformSlimeTarget).GetComponent<SlimeTarget>();
                if(levelData.conditionSlimes.lsDataSlime.Count > 4)
                {
                    temp.transform.parent = tranformSlimeTargetLarge.transform;
                }
                else
                {
                    temp.transform.parent = tranformSlimeTarget.transform;
                }
          

                temp.Init(item.countSlime);
                lsCurrentSlimeTargets.Add(temp);
            }
            foreach (var item in levelData.conditionSlimes.lsDataSlime)
            {
                var temp = Instantiate(GetSlimeTargetPrefab(item.slimeType), tranformSlimeTarget).GetComponent<SlimeTarget>();
                temp.transform.parent = tranformSlimePrepage.transform;
                temp.Init(item.countSlime);
         
            }
        }


     
    }
    public bool isLose
    {
       get
        {
            foreach (var item in lsCurrentSlimeTargets)
            {
                if (!item.isComplete)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public void HanldCheckWin()
    {
        foreach (var item in lsCurrentSlimeTargets)
        {
            if (!item.isComplete)
            {
                return;
            }
        }
        GamePlayController.Instance.stateGame = StateGame.Win;
        Winbox.Setup().Show();       
    }

}
