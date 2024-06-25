using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PackInShopAds : PackInShop
{
  
    public GiftType currentGift;
    ActionWatchVideo actionWatchVideo;
    public GameObject decorBtn;
    public int numGift = 1;
    public GameObject panelTime;
    
    public  bool WasWatch
    {
        get
        {
     
            return PlayerPrefs.GetInt("WasWatchPackInShopAds"   + currentGift.ToString(), 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("WasWatchPackInShopAds" + currentGift.ToString(), value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public override void Init()
    {
        btnBuy.onClick.AddListener(HandleOnClick);
        if(!WasWatch)
        {
            decorBtn.gameObject.SetActive(true);
            if(panelTime != null)
            {
                decorBtn.gameObject.SetActive(false);
            }
        }
        else
        {
            decorBtn.gameObject.SetActive(false);
            if (panelTime != null)
            {
                decorBtn.gameObject.SetActive(true);
            }
        }

    }

    private void HandleOnClick()
    {
      if(!WasWatch )
        {
            GameController.Instance.admobAds.ShowVideoReward(
                    actionReward: () =>
                    {


                        Claim();

                    },
                    actionNotLoadedVideo: () =>
                    {
                        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                         (
                            btnBuy.transform
                            ,
                         btnBuy.transform.position,
                         "No video at the moment!",
                         Color.white,
                         isSpawnItemPlayer: true
                         );
                    },
                    actionClose: null,
                    actionWatchVideo,
                    UseProfile.CurrentLevel.ToString());
        }    
      else
        {
            GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp_UI
                                   (
                                      btnBuy.transform
                                      ,
                                   btnBuy.transform.position,
                                   "Wait more time!",
                                   Color.white,
                                   isSpawnItemPlayer: true
                                   );
        }    
      


        void Claim()
        {
            switch (currentGift)
            {
                case GiftType.TNT_Booster:

                    actionWatchVideo = ActionWatchVideo.TNT_Booster;
                    break;
                case GiftType.Rocket_Booster:

                    actionWatchVideo = ActionWatchVideo.Rocket_Booster;
                    break;
                case GiftType.Freeze_Booster:

                    actionWatchVideo = ActionWatchVideo.Freeze_Booster;
                    break;
                case GiftType.Atom_Booster:

                    actionWatchVideo = ActionWatchVideo.Atom_Booste;
                    break;
            }
            List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
            giftRewardShows.Add(new GiftRewardShow() { amount = numGift, type = currentGift });
            foreach(var item in giftRewardShows)
            {
                GameController.Instance.dataContain.giftDatabase.Claim(item.type, item.amount);
            }    
            PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { });
            decorBtn.gameObject.SetActive(false);
            WasWatch = true;
        }



     
    }    

    public void HandleOn()
    {
        WasWatch = false;
        decorBtn.gameObject.SetActive(true);
    }    
}
