using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Rocket_Booster : MonoBehaviour
{
    public Button rocket_Btn;
    public Text tvNum;
    public GameObject objNum;
    public GameObject parentTvCoin;
    public GameObject lockIcon;
    public GameObject unLockIcon;
    public RocketVfx rocketVfx;
    public bool wasUseTNT_Booster;
    public Transform post;

 

    public void Init()
    {
       
        wasUseTNT_Booster = false;
        if (UseProfile.CurrentLevel >= 5)
        {

            //unLockIcon.gameObject.SetActive(true);
            lockIcon.gameObject.SetActive(false);
            HandleUnlock();
            Debug.LogError("HandleUnlock");

        }
        else
        {
            //unLockIcon.gameObject.SetActive(false);
            lockIcon.gameObject.SetActive(true);
            objNum.SetActive(false);
            HandleLock();
            Debug.LogError("HandleLock");
        }


        void HandleUnlock()
        {
            rocket_Btn.onClick.AddListener(HandleTNT_Booster);
            if (UseProfile.Roket_Booster > 0)
            {
                objNum.SetActive(true);
                tvNum.text = UseProfile.Roket_Booster.ToString();
                parentTvCoin.SetActive(false);
            }
            else
            {
                objNum.SetActive(false);
                tvNum.gameObject.SetActive(false);
                parentTvCoin.SetActive(true);
            }
            EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.CHANGE_ROCKET_BOOSTER, ChangeText);
        }
        void HandleLock()
        {


            rocket_Btn.onClick.AddListener(HandleLockBtn);
        }
    }

    public void HandleLockBtn()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                              (
                              rocket_Btn.transform.position,
                              "Unlock at level 2",
                              Color.white,
                              isSpawnItemPlayer: true
                              );
    }





    public void HandleTNT_Booster()
    { 
        if (UseProfile.Roket_Booster >= 1)
        {
            GamePlayController.Instance.playerContain.tutorial_Rocket.NextTut();
            var target = GamePlayController.Instance.playerContain.levelData.GetRandomSlimeForRocketBooster;
            if(target != null)
            {
                UseProfile.Roket_Booster -= 1;
                rocket_Btn.interactable = false;
                var rocket = SimplePool2.Spawn(rocketVfx, post.position, Quaternion.identity);
                rocket.transform.parent = target.transform;
                rocket.transform.localEulerAngles = new Vector3(0, 0, -137);
                rocket.transform.DOLocalMove(Vector3.zero, 0.7f).OnComplete(delegate {


                    rocket.HandleDisable();
                    target.TakeDame();
                    rocket_Btn.interactable = true;
                });
            } 
        }
        else
        {
            SuggetBox.Setup(GiftType.Rocket_Booster).Show();
        }
       


    }


 

    public void ChangeText(object param)
    {
        tvNum.text = UseProfile.Roket_Booster.ToString();
        if (UseProfile.Roket_Booster > 0)
        {
            objNum.SetActive(true);
            tvNum.gameObject.SetActive(true);
            tvNum.text = UseProfile.Roket_Booster.ToString();
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
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.CHANGE_ROCKET_BOOSTER, ChangeText);
    }
}
