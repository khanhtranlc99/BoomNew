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
 
    public Transform post;
    public GameObject vfxFreeze;
    public CanvasGroup canvasGroup;
    public GameObject vfxUI;
    public GameObject parent;
    public void Init()
    {

 
        if (UseProfile.CurrentLevel >= 7)//7
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
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.Freeze_Booster >= 1)
        {
            GamePlayController.Instance.playerContain.tutorial_Freeze.NextTut();
            UseProfile.Freeze_Booster -= 1;
            OnVfx();
        }
        else
        {
            SuggetBox.Setup(GiftType.Freeze_Booster).Show();
        }

    }

    public void OnVfx()
    {
        var freeze = SimplePool2.Spawn(vfxFreeze );
        freeze.transform.position = freezeBooster.transform.position;
        freeze.transform.parent =  GamePlayController.Instance.gameScene.canvas.transform;
        freeze.transform.localScale = new Vector3(0, 0, 0);
        freeze.transform.DOScale(new Vector3(2, 2, 2), 0.3f).OnComplete(delegate {
           
            freeze.transform.DOMove(parent.transform.position, 0.5f).SetEase(Ease.InBack).OnComplete(delegate
            {
              
                canvasGroup.DOFade(1, 0.3f).OnComplete(delegate
                {
                    GamePlayController.Instance.playerContain.levelData.HandleFreezeBooster();
                    vfxUI.SetActive(true);
                 
                    Debug.LogError("freezelocalScale");
                });
                SimplePool2.Despawn(freeze.gameObject);
            });
        });


     
    }
    public void OffVfx()
    {
        vfxUI.gameObject.SetActive(false);
        canvasGroup.DOFade(0, 0.3f);
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
