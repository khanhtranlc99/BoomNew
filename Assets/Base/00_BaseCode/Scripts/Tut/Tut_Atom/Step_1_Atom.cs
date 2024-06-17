using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_Atom : TutorialBase
{
    GameObject currentHand;
    public override bool IsCanEndTut()
    {
        Destroy(currentHand.gameObject);
        return true;

    }

    public override void StartTut()
    {
        if (UseProfile.CurrentLevel == 9)
        {
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.position = GamePlayController.Instance.playerContain.atomBoom_Booster.btnAtom_Booster.transform.position;

        }
    }

    protected override void SetNameTut()
    {

    }
}
