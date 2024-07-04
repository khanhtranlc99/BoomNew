using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_Rocket : TutorialBase
{
    GameObject currentHand;
    public override bool IsCanEndTut()
    {
        if (currentHand != null)
        {
            Destroy(currentHand.gameObject);
        }
        return base.IsCanShowTut();

    }

    public override void StartTut()
    {
       
        if (UseProfile.CurrentLevel == 5)
        {
            if (currentHand != null)
            {
                return;
            }
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.parent = GamePlayController.Instance.playerContain.rocket_Booster.rocket_Btn.transform;
            currentHand.transform.localScale = new Vector3(1, 1, 1);
            currentHand.transform.position = GamePlayController.Instance.playerContain.rocket_Booster.rocket_Btn.transform.position;
            currentHand.transform.localEulerAngles = new Vector3(0, 0, 120);
            currentHand.transform.position = new Vector3(post.x + 0.5f, post.y + 0.7f, post.z);
        }
    }
    Vector3 post
    {
        get
        {
            return GamePlayController.Instance.playerContain.rocket_Booster.rocket_Btn.transform.position;
        }
    }
    protected override void SetNameTut()
    {

    }
}
