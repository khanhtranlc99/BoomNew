using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ShopBox : BaseBox
{
    private static ShopBox instance;
    public static ShopBox Setup(ButtonShopType param = ButtonShopType.Gift, bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<ShopBox>(PathPrefabs.SHOP_BOX));
            instance.Init(param);
        }

        instance.InitState();
        return instance;
    }
    public ButtonShopController shopController;
    public List<PackInShop> lsPackInShops;
    public List<PackInShopAds> lsPackInShopAds;
    public long countTime;
    public bool wasCountTime;
    public Text tvCountTime;
    public Text tvCountTime_2;
    public Text tvCountTimePackCoin;
    public Text tvCountTimePackCoin_2;
    public CoinHeartBar coinHeartBar;
    private void Init(ButtonShopType buttonShopType)
    {
        shopController.Init(buttonShopType);
        foreach(var item in lsPackInShops)
        {
            item.Init();
        }
        coinHeartBar.Init();
    }
    private void InitState()
    {
        ResetDay();
    }

    private void ResetDay()
    {
        wasCountTime = false;
        countTime = TimeManager.CaculateTime(TimeManager.ParseTimeStartDay(UseProfile.LastTimeOnline),
          TimeManager.ParseTimeStartDay(DateTime.Now));
        if (countTime >= 86400)
        {
            UseProfile.LastTimeOnline = DateTime.Now;
            foreach(var item in lsPackInShopAds)
            {
                item.HandleOn();
            }    

        }
        wasCountTime = true; 
    }
    private void Update()
    {
        if(wasCountTime)
        {
           if(countTime > 0)
            {
                tvCountTime.text = "REFRESH IN :" + TimeManager.ShowTime2(countTime);
                tvCountTime_2.text = "REFRESH IN :" + TimeManager.ShowTime2(countTime);
                tvCountTimePackCoin.text =   TimeManager.ShowTime2(countTime);
                tvCountTimePackCoin_2.text =    TimeManager.ShowTime2(countTime);
            }
        }
    }
}
