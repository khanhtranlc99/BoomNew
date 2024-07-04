using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class PlayerContain : MonoBehaviour
{
    public LevelData levelData;
    public BoomInputController boomInputController;
    public WinStreakController winStreakController;
    public TNT_Booster TNT_Booster;
    public AtomBoom_Booster atomBoom_Booster;
    public Rocket_Booster rocket_Booster;
    public Freeze_Booster freeze_Booster;
    public FlameUp_Item flameUp_Item;
    public FastBoom_Item fastBoom_Item;
    public TimeBoom_Item timeBoom_Item;
    public PopupPrepageGame prepageGame;
    public TutorialFunController tutorial_BoomInput;
    public TutorialFunController tutorial_TNT;
    public TutorialFunController tutorial_Rocket;
    public TutorialFunController tutorial_Freeze;
    public TutorialFunController tutorial_Atom;
    public TutorialFunController tutorial_FastBoom;
    public TutorialFunController tutorial_TimeBoom;
    public CameraScale cameraScale;
    public int totalCoin;

    public void Init()
    {
        totalCoin = 0;
        UseProfile.FlameUp_Item = 0;
        UseProfile.TimeBoom_Item = 0;
        UseProfile.FastBoom_Item = 0;
        string pathLevel = StringHelper.PATH_CONFIG_LEVEL_TEST;
        levelData = Instantiate(Resources.Load<LevelData>(string.Format(pathLevel, UseProfile.CurrentLevel)));
        cameraScale.Init();
        GamePlayController.Instance.gameScene.Init(levelData);
        prepageGame.Init(delegate
        {
            if (UseProfile.WinStreak > 0)
            {
                winStreakController.Init(delegate
                {
                    levelData.Init(true);
            
                    SetUp();
                });
            }
            else
            {
                levelData.Init();
              
                SetUp();
            }

        });
        //levelData.Init(true);
        //cameraScale.Init();
        //SetUp();
        void SetUp()
        {
            boomInputController.Init(levelData);
            TNT_Booster.Init();
            atomBoom_Booster.Init();
            rocket_Booster.Init();
            freeze_Booster.Init();
            flameUp_Item.Init();
            fastBoom_Item.Init();
            timeBoom_Item.Init();
            tutorial_Atom.Init();
            tutorial_Freeze.Init();
            tutorial_Rocket.Init();
            tutorial_TNT.Init();
            tutorial_BoomInput.Init();
            tutorial_BoomInput.StartTut();
            tutorial_FastBoom.Init();
            tutorial_TimeBoom.Init();
        }
    }

    



}
