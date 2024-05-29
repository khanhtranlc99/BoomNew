﻿using Crystal;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateGame
{
    Loading = 0,
    Playing = 1,
    Win = 2,
    Lose = 3,
    Pause = 4
}

public class GamePlayController : Singleton<GamePlayController>
{
    public PlayerContain playerContain;
    public GameScene gameScene;

 

    protected override void OnAwake()
    {
      //  GameController.Instance.currentScene = SceneType.GamePlay;

        Init();

    }

    public void Init()
    {
        playerContain.Init();
        gameScene.Init();
        //GameController.Instance.AnalyticsController.LoadingComplete();
        //GameController.Instance.admobAds.canShowOpenAppAds = true;
        //if (Application.internetReachability != NetworkReachability.NotReachable)
        //{
        //    GameController.Instance.admobAds.ShowOpenAppAdsInGame();
        //}
    }

  

}
