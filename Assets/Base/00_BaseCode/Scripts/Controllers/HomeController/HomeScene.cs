﻿using MoreMountains.NiceVibrations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class HomeScene : BaseScene
{

    public Button btnSetting;
 

    public Button btnPlay;
    public Button btnShop;

   
    public CoinHeartBar coinHeartBar;
    public QuestBar questBar;

    public InfoDataLevel infoDataLevel;
    public void ShowGift()
    {
        

    }
    public int NumberPage(ButtonType buttonType)
    {
        switch (buttonType)
        {
            case ButtonType.ShopButton:
                return 0;
                break;

            case ButtonType.HomeButton:
                return 1;
                break;

            case ButtonType.RankButton:
                return 2;
                break;

        }
        return 0;
    }


    public void Init()
    {
        coinHeartBar.Init();
        questBar.Init();
      
    
        btnSetting.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); OnSettingClick(); });

        btnPlay.onClick.AddListener(delegate { InfoLevelBox.Setup(infoDataLevel.lsInfoDatas[UseProfile.CurrentLevel - 1]).Show(); });

        btnShop.onClick.AddListener(delegate { ShopBox.Setup().Show(); });


    }
    //private void Update()
    //{

    //       // OnScreenChange();


    //}
    
    

   

    public override void OnEscapeWhenStackBoxEmpty()
    {
        //Hiển thị popup bạn có muốn thoát game ko?
    }
    private void OnSettingClick()
    {
        SettingBox.Setup(false).Show();
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    


}
