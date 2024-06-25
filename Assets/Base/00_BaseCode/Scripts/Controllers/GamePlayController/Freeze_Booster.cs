using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Freeze_Booster : MonoBehaviour
{
    public Button freezeBooster;
    public Text tvNum;
    public GameObject objNum;
    public GameObject parentTvCoin;
    public GameObject lockIcon;
    public GameObject unLockIcon;
    public RocketVfx rocketVfx;
    public Transform post;



    public void Init()
    {

 
        if (UseProfile.CurrentLevel >= 7)
        {

            //unLockIcon.gameObject.SetActive(true);
            lockIcon.gameObject.SetActive(false);
            HandleUnlock();
        

        }
        else
        {
            //unLockIcon.gameObject.SetActive(false);
            lockIcon.gameObject.SetActive(true);
            objNum.SetActive(false);
            HandleLock();
        
        }


        void HandleUnlock()
        {
            freezeBooster.onClick.AddListener(HandleFreezeBooster);
            if (UseProfile.Freeze_Booster > 0)
            {
                objNum.SetActive(true);
                tvNum.text = UseProfile.Freeze_Booster.ToString();
                parentTvCoin.SetActive(false);
            }
            else
            {
                objNum.SetActive(false);
                tvNum.gameObject.SetActive(false);
                parentTvCoin.SetActive(true);
            }
            EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.CHANGE_FREEZE_BOOSTER, ChangeText);
        }
        void HandleLock()
        {
            freezeBooster.onClick.AddListener(HandleLockBtn);
        }
    }

    public void HandleLockBtn()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                              (
                              freezeBooster.transform.position,
                              "Unlock at level 2",
                              Color.white,
                              isSpawnItemPlayer: true
                              );
    }





    public void HandleFreezeBooster()
    {
        if(UseProfile.Freeze_Booster >= 1)
        {
            GamePlayController.Instance.playerContain.tutorial_Freeze.NextTut();
            UseProfile.Freeze_Booster -= 1;
            GamePlayController.Instance.playerContain.levelData.HandleFreezeBooster();
        }
        else
        {
            SuggetBox.Setup(GiftType.Freeze_Booster).Show();
        }

    }




    public void ChangeText(object param)
    {
        tvNum.text = UseProfile.Freeze_Booster.ToString();
        if (UseProfile.Freeze_Booster > 0)
        {
            objNum.SetActive(true);
            tvNum.gameObject.SetActive(true);
            tvNum.text = UseProfile.Freeze_Booster.ToString();
            parentTvCoin.SetActive(false);
        }
        else
        {
            objNum.SetActive(false);
            tvNum.gameObject.SetActive(false);
            parentTvCoin.SetActive(true);
        }
    }
    public void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.CHANGE_FREEZE_BOOSTER, ChangeText);
    }
}
