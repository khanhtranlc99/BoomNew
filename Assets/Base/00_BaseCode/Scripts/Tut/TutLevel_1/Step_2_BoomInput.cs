using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_2_BoomInput : TutorialBase
{
    GameObject currentHand;
    public Tutorial_BoomInput tutorial_BoomInput;
    bool wasStart = false;

    public override bool IsCanEndTut()
    {
        if (currentHand != null)
        {
            Destroy(currentHand.gameObject);
  
        }
        if(wasStart)
        {
            tutorial_BoomInput.HandleSpawnBoss();
        }    
        //if (PlayerPrefs.GetInt(StringHelper.IS_DONE_TUT + "Tutorial_BoomInput_Step_1") == 0)
        //{
        //    return false;
        //}    
        return true ;
    }
    //public override bool IsCanShowTut()
    //{

    //    if (currentHand == null)
    //    {
    //        return false;

    //    }
    //    return base.IsCanShowTut();
    //}

    public override void StartTut()
    {
     
        if (UseProfile.CurrentLevel == 1)
        {
            if (currentHand != null)
            {
                return;
            }
            currentHand = SimplePool2.Spawn(handTut);
            currentHand.transform.position = GamePlayController.Instance.playerContain.levelData.gridBasesId[14].transform.position;
            currentHand.transform.position += new Vector3(-0.5f, 0.5f, 0);
            wasStart = true;
        }
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
