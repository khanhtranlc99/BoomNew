using Crystal;
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
    public StateGame stateGame;
    public PlayerContain playerContain;
    public GameScene gameScene;
    public ItemInGame itemInGame;
    public Flame flame;


    protected override void OnAwake()
    {
        //  GameController.Instance.currentScene = SceneType.GamePlay;

     
        Init();

    }

    public void Init()
    {
     

            playerContain.Init();
        SimplePool2.ClearPool();
        SimplePool2.Preload(itemInGame.gameObject, 10, this.transform);
        SimplePool2.Preload(flame.gameObject, 50, this.transform);
        stateGame = StateGame.Playing;
        //GameController.Instance.AnalyticsController.LoadingComplete();
        //GameController.Instance.admobAds.canShowOpenAppAds = true;
        //if (Application.internetReachability != NetworkReachability.NotReachable)
        //{
        //    GameController.Instance.admobAds.ShowOpenAppAdsInGame();
        //}
    }
    public void HandleCheckLose()
    {
        if (stateGame == StateGame.Playing)
        {
            if (playerContain.boomInputController.countBoom <= 0 && gameScene.targetController.isLose)
            {
                GamePlayController.Instance.playerContain.boomInputController.enabled = true;
                if(stateGame == StateGame.Playing)
                {
                    stateGame = StateGame.Lose;
                    LoseBox.Setup().Show();
                }    
          
            }
        }
    }
  

}
