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

    public Text tvDifficut;
    public Image imgLevelType;
    public Sprite easySprite;
    public Sprite hardSprite;
    public Sprite veryHardSprite;

    [Header("Test")]
    public GameObject testObj;
    public InputField levelTestTxt;

    public void Init(LevelData levelData)
    {
        targetController.Init(levelData);
        settinBtn.onClick.AddListener(delegate { SettingBox.Setup(true).Show(); });
        tvLevel.text = "Level "   + UseProfile.CurrentLevel;
        switch (levelData.difficult)
        {
            case Difficult.Normal:
                tvDifficut.text = "Esasy";
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
    public void HandleOnCheatLevel()
    {
        if(testObj.activeInHierarchy)
        {
            testObj.SetActive(false);
        }   
        else
        {
            testObj.SetActive(true);
        }
       
    }    
    public void OnClickTest()
    {
        int level = System.Int32.Parse(levelTestTxt.text);      
        UseProfile.CurrentLevel = level;
        GameController.Instance.LoadScene(SceneName.GAME_PLAY);
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
