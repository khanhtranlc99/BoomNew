using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_TNT : TutorialBase
{
    GameObject currentHand;
    public override bool IsCanEndTut()
    {
        Destroy(currentHand.gameObject);
        return true;

    }

    public override void StartTut()
    {
        if (UseProfile.CurrentLevel == 3)
        {
            Debug.LogError("StartTut");
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.parent = GamePlayController.Instance.playerContain.TNT_Booster.btnTNT_Booster.transform;
            currentHand.transform.localScale = new Vector3(1, 1, 1);
            currentHand.transform.position = GamePlayController.Instance.playerContain.TNT_Booster.btnTNT_Booster.transform.position;
        }
    }

    protected override void SetNameTut()
    {
     
    }
}
