using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Winbox : BaseBox
{
    public static Winbox _instance;
    public static Winbox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<Winbox>(PathPrefabs.WIN_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }

    public Button nextButton;
    public Button rewardButton;
    public CoinHeartBar coinHeartBar;
    public Text tvCoin;

    public void Init()
    {
        nextButton.onClick.AddListener(delegate { HandleNext();    });
        rewardButton.onClick.AddListener(delegate { HandleReward(); });

        tvCoin.text = "" + GamePlayController.Instance.playerContain.totalCoin;
        coinHeartBar.Init();
        UseProfile.WinStreak += 1;
    }   
    public void InitState()
    {
       
     


    }    
    private void HandleNext()
    {
        UseProfile.CurrentLevel += 1;
        UseProfile.Coin += GamePlayController.Instance.playerContain.totalCoin;
        Initiate.Fade("GamePlay", Color.black, 2f);
    }    
    private void HandleReward()
    {
    
        GameController.Instance.admobAds.ShowVideoReward(
                   actionReward: () =>
                   {
                       GamePlayController.Instance.playerContain.totalCoin *= 5;
                       UseProfile.Coin += GamePlayController.Instance.playerContain.totalCoin;
                       UseProfile.CurrentLevel += 1;
                       List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
                       giftRewardShows.Add(new GiftRewardShow() { amount = GamePlayController.Instance.playerContain.totalCoin, type = GiftType.Coin });
                       PopupRewardBase.Setup(false).Show(giftRewardShows, delegate {

                           Initiate.Fade("GamePlay", Color.black, 2f);
                       });

                   },
                   actionNotLoadedVideo: () =>
                   {
                       GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                        (rewardButton.transform,
                        rewardButton.transform.position,
                        "No video at the moment!",
                        Color.white,
                        isSpawnItemPlayer: true
                        );
                   },
                   actionClose: null,
                   ActionWatchVideo.HeartInHearPopup,
                   UseProfile.CurrentLevel.ToString());
    }    

}
