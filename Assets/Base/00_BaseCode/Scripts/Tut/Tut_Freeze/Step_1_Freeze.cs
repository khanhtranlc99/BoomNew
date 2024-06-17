using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_Freeze : TutorialBase
{
    GameObject currentHand;
    public override bool IsCanEndTut()
    {
        Destroy(currentHand.gameObject);
        return true;

    }

    public override void StartTut()
    {
        if (UseProfile.CurrentLevel == 7)
        {
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.position = GamePlayController.Instance.playerContain.freeze_Booster.freezeBooster.transform.position;

        }
    }

    protected override void SetNameTut()
    {

    }
}

