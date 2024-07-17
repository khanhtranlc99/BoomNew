using MoreMountains.NiceVibrations;
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
    public Text tvLevel;
    public Text tvDifficut;
    public Image imgLevelType;
    public Sprite easySprite;
    public Sprite hardSprite;
    public Sprite veryHardSprite;
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

        btnPlay.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound();  InfoLevelBox.Setup(infoDataLevel.lsInfoDatas[UseProfile.CurrentLevel - 1]).Show(); });

        btnShop.onClick.AddListener(delegate { GameController.Instance.musicManager.PlayClickSound(); ShopBox.Setup().Show(); });

        tvLevel.text = "LEVEL " + UseProfile.CurrentLevel.ToString();
        switch (infoDataLevel.lsInfoDatas[UseProfile.CurrentLevel - 1].difficult)
        {
            case Difficult.Normal:
                tvDifficut.text = "Easy";
                imgLevelType.sprite = easySprite;
                break;
            case Difficult.Hard:
                tvDifficut.text = "Hard";
                imgLevelType.sprite = hardSprite;
                break;
            case Difficult.VeryHard:
                tvDifficut.text = "VeryHard";
                imgLevelType.sprite = veryHardSprite;
                break;

        }

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
        //MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    


}
