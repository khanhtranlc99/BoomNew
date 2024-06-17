using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_Rocket : TutorialBase
{
    GameObject currentHand;
    public override bool IsCanEndTut()
    {
        Destroy(currentHand.gameObject);
        return true;

    }

    public override void StartTut()
    {
        if (UseProfile.CurrentLevel == 5)
        {
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.position = GamePlayController.Instance.playerContain.rocket_Booster.rocket_Btn.transform.position;

        }
    }

    protected override void SetNameTut()
    {

    }
}
