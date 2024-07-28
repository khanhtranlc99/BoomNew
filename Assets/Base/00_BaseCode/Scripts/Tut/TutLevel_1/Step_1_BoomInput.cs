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

     return true; 
    }

    public override void StartTut()
    {
     
        if (UseProfile.CurrentLevel == 1)
        {
            if (currentHand != null)
            {
                return;
            }
            GamePlayController.Instance.playerContain.boomInputController.enabled = false;
            Invoke(nameof(HandleSetup),2);
        }
    }
    private void HandleSetup()
    {
        GamePlayController.Instance.playerContain.boomInputController.enabled = true;
        currentHand = SimplePool2.Spawn(handTut);
        currentHand.transform.position = GamePlayController.Instance.playerContain.levelData.gridBasesId[19].transform.position;
        currentHand.transform.position += new Vector3(-0.5f, 0.5f, 0);
    }    
    public void DeleteHand()
    {
        if (currentHand != null)
        {
            Destroy(currentHand.gameObject);

        }

    }

    protected override void SetNameTut()
    {
     
    }
    public override void OnEndTut()
    {
      
    }
}
