using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_BoomInput : TutorialBase
{
    GameObject currentHand;
    public override bool IsCanEndTut()
    {
        if(currentHand != null)
        {
            Destroy(currentHand.gameObject);
        }

     return base.IsCanShowTut(); ;
    }

    public override void StartTut()
    {
        Debug.LogError("Step_1_BoomInput");
        if (UseProfile.CurrentLevel == 1)
        {
            if (currentHand != null)
            {
                return;
            }
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.position = GamePlayController.Instance.playerContain.levelData.gridBasesId[19].transform.position;

        }
    }

    protected override void SetNameTut()
    {
     
    }
}
