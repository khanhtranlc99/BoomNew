using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoseBox : BaseBox
{
    public static LoseBox _instance;
    public static LoseBox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<LoseBox>(PathPrefabs.LOSE_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }
    public Text tvScore;
    public Button btnClose;
    public Button btnAdsRevive;
    public Button btnReviveByCoin;
    public AudioClip loseClip;
    public CoinHeartBar coinHeartBar;

    public void Init()
    {
        btnClose.onClick.AddListener(delegate { HandleClose(); });
        btnAdsRevive.onClick.AddListener(delegate { HandleAdsRevive(); });
        btnReviveByCoin.onClick.AddListener(delegate { HandleReviveByCoin(); });
        coinHeartBar.Init();
    }   
    public void InitState()
    {
        
    }
    public void HandleReviveByCoin()
    {
        if (UseProfile.Coin >= 100)
        {
            UseProfile.Coin -= 100;
            GamePlayController.Instance.stateGame = StateGame.Playing;
            GamePlayController.Instance.playerContain.boomInputController.HandlePlus(5);
            GamePlayController.Instance.playerContain.boomInputController.enabled = true;
         
 
            Close();
        }
        else
        {
            ShopCoinBox.Setup().Show();
        }


    }
    public void HandleAdsRevive()
    {
        GameController.Instance.admobAds.ShowVideoReward(
                    actionReward: () =>
                    {
                        GamePlayController.Instance.stateGame = StateGame.Playing;
                        GamePlayController.Instance.playerContain.boomInputController.HandlePlus(5);
                        GamePlayController.Instance.playerContain.boomInputController.enabled = true;
                 
                        Close();
                    },
                    actionNotLoadedVideo: () =>
                    {
                        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                         (btnAdsRevive.transform
                            ,
                         btnAdsRevive.transform.position,
                         "No video at the moment!",
                         Color.white,
                         isSpawnItemPlayer: true
                         );
                    },
                    actionClose: null,
                    ActionWatchVideo.ReviveFreeLoseBox,
                    UseProfile.CurrentLevel.ToString());



    }
    public void HandleClose()
    {

        //Close();
        BackHomeBox.Setup(TypeBackHOme.BackHome).Show();

    }

}
