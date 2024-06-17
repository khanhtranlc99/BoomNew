using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_TNT : TutorialBase
{
    GameObject currentHand;
     
    public override bool IsCanEndTut()
    {
        Destroy(currentHand.gameObject);
        return base.IsCanShowTut();

    }

    public override void StartTut()
    {
        if (UseProfile.CurrentLevel == 3)
        {
           if(currentHand != null)
            {
                return;
            }                
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.parent = GamePlayController.Instance.playerContain.TNT_Booster.btnTNT_Booster.transform;
            currentHand.transform.localScale = new Vector3(1, 1, 1);
            currentHand.transform.localEulerAngles = new Vector3(0, 0, 120);
            currentHand.transform.position = new Vector3(post.x +0.5f, post.y + 0.7f, post.z);
        }
    }
    Vector3 post
    {
        get
        {
            return GamePlayController.Instance.playerContain.TNT_Booster.btnTNT_Booster.transform.position;
        }
    }    


    protected override void SetNameTut()
    {
     
    }
}
