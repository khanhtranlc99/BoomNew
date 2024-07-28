using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_TimeBoom : TutorialBase
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
        if (GamePlayController.Instance.playerContain.timeBoom_Item.btnTimeBoom.gameObject.activeSelf)
        {
            if (currentHand != null)
            {
                return;
            }
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.parent = GamePlayController.Instance.playerContain.timeBoom_Item.btnTimeBoom.transform;
            currentHand.transform.localScale = new Vector3(3, 3, 3);
            currentHand.transform.position = GamePlayController.Instance.playerContain.timeBoom_Item.btnTimeBoom.transform.position;

            currentHand.transform.position = new Vector3(post.x +0.5f, post.y - 1, post.z);
        }
    }
    Vector3 post
    {
        get
        {
            return GamePlayController.Instance.playerContain.timeBoom_Item.btnTimeBoom.transform.position;
        }
    }
    protected override void SetNameTut()
    {

    }
    public override void OnEndTut()
    {

    }

}