using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public List<SlimeTarget> lsSlimeTargets;
    public Transform tranformSlimeTarget;
    public List<SlimeTarget> lsCurrentSlimeTargets;

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
                temp.Init(item.countSlime);
                lsCurrentSlimeTargets.Add(temp);
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
