using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using MoreMountains.NiceVibrations;
using UnityEngine.Events;

public class GameScene : BaseScene
{
    public TargetController targetController;
    public Text tvLevel;
    public Button settinBtn;
    public Transform canvas;
    public CanvasGroup canvasGroupBot;
    public GameObject botObj;
  


    public void Init(LevelData levelData)
    {
        targetController.Init(levelData);
        settinBtn.onClick.AddListener(delegate { SettingBox.Setup().Show(); });
    }

    public void HideBotUI( Action callBack)
    {
        canvasGroupBot.DOFade(0, 0.5f).OnComplete(delegate {
            callBack?.Invoke();
            botObj.SetActive(false);
        });
    }
    public void ShowBotUI(Action callBack)
    {
        canvasGroupBot.DOFade(1, 0.5f).OnComplete(delegate {
            callBack?.Invoke();
            botObj.SetActive(true);
        });
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {
         
    }
}
