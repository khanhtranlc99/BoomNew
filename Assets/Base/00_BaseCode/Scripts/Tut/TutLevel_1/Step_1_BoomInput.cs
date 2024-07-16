using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_1_BoomInput : TutorialBase
{
    GameObject currentHand;
    public GameObject panelTut;
    public override bool IsCanEndTut()
    {
        if(currentHand != null)
        {
            Destroy(currentHand.gameObject);
            panelTut.SetActive(false);
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
            currentHand.transform.position = GamePlayController.Instance.playerContain.levelData.gridBasesId[18].transform.position;
            panelTut.SetActive(true);

        }
    }

    protected override void SetNameTut()
    {
     
    }
}
