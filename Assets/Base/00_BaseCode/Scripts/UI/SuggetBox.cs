using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SuggetBox : BaseBox
{
    public static SuggetBox _instance;
    public static SuggetBox Setup(GiftType giftType, bool isBoosterTut = false)
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<SuggetBox>(PathPrefabs.SUGGET_BOX));
            _instance.Init();
        }
        _instance.InitState( giftType, isBoosterTut);
        return _instance;
    }

    public Button btnClose;
    public Button payByCoinBtn;
    public Button payByAdsBtn;
    public Text tvTitler;
    public Text tvContent;
    public Text tvPrive;
    public int price;
    GiftType currentGift;
    ActionWatchVideo actionWatchVideo;
    public Image iconDecor;
    public Text tvNum;
    public void Init()
    {
        btnClose.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); GamePlayController.Instance.playerContain.boomInputController.enabled = true;Close(); });
        payByAdsBtn.onClick.AddListener(delegate { HandlePayByAds(); });
        payByCoinBtn.onClick.AddListener(delegate { HandlePayByCoin(); });
    }

    public void InitState(GiftType giftType, bool isTut)
    {
        currentGift = giftType;
        switch (giftType)
        {
            case GiftType.TNT_Booster:
                tvTitler.text = "TNT BOOM";
                tvContent.text = "TAP ANY TILE STACK TO CLEAR IT";
                price = 50;
                tvPrive.text = price.ToString();
                actionWatchVideo = ActionWatchVideo.TNT_Booster;
                break;
            case GiftType.Rocket_Booster:
                tvTitler.text = "Rocket";
                tvContent.text = "MOVE AND REPLACE A TILE STACK ON THE BROAD";
                price = 50;
                tvPrive.text = price.ToString();
                actionWatchVideo = ActionWatchVideo.Rocket_Booster;
                break;
            case GiftType.Freeze_Booster:
                tvTitler.text = "Freeze";
                tvContent.text = "REFRESH TRAY TO GET NEW STACK OPPTIONS";
                price = 120;
                tvPrive.text = price.ToString();
                actionWatchVideo = ActionWatchVideo.Freeze_Booster;
                break;
            case GiftType.Atom_Booster:
                tvTitler.text = "Atom";
                tvContent.text = "REFRESH TRAY TO GET NEW STACK OPPTIONS";
                price = 200;
                tvPrive.text = price.ToString();
                actionWatchVideo = ActionWatchVideo.Atom_Booste;
                break;
        }
        iconDecor.sprite = GameController.Instance.dataContain.giftDatabase.GetIconItem(giftType);
        iconDecor.SetNativeSize();
        if (isTut)
        {
            payByAdsBtn.gameObject.SetActive(false);
            payByCoinBtn.gameObject.SetActive(false);
            tvNum.gameObject.SetActive(false);
        }    
        else
        {
            payByAdsBtn.gameObject.SetActive(true);
            payByCoinBtn.gameObject.SetActive(true);
            tvNum.gameObject.SetActive(true);

        }
        GamePlayController.Instance.playerContain.boomInputController.enabled = false;
    }

    public void HandlePayByAds()
    {

        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.admobAds.ShowVideoReward(
                     actionReward: () =>
                     {

                         HandleClaimGiftX1();


                     },
                     actionNotLoadedVideo: () =>
                     {
                         GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                          (
                             payByAdsBtn.transform
                             ,
                          payByAdsBtn.transform.position,
                          "No video at the moment!",
                          Color.white,
                          isSpawnItemPlayer: true
                          );
                     },
                     actionClose: null,
                     actionWatchVideo,
                     UseProfile.CurrentLevel.ToString());
    }   
    
    public void HandlePayByCoin()
    {
        GameController.Instance.musicManager.PlayClickSound();
        if (UseProfile.Coin >= price)
        {
            UseProfile.Coin -= price;      
            HandleClaimGift();
        }
        else
        {
            ShopBox.Setup(ButtonShopType.Gold).Show();
        }    


    }  
    

    public void HandleClaimGift()
    {
   
        GamePlayController.Instance.playerContain.boomInputController.enabled = true;
        Close();
        GameController.Instance.dataContain.giftDatabase.Claim(currentGift, 1);
        List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
        giftRewardShows.Add(new GiftRewardShow() { amount = 1, type = currentGift });
        PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { });

    }
    public void HandleClaimGiftX1()
    {

        GamePlayController.Instance.playerContain.boomInputController.enabled = true;
        Close();
        GameController.Instance.dataContain.giftDatabase.Claim(currentGift, 1);
        List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
        giftRewardShows.Add(new GiftRewardShow() { amount = 1, type = currentGift });
        PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { });

    }
}
