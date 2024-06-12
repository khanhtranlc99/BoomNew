using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : SceneBase
{
    public Button btnHome;
    public Text tvLevel;
    public RandomWatchVideo btnReward;
    public override void Init()
    {
        tvLevel.text = "Level " + UseProfile.CurrentLevel;
        btnHome.onClick.AddListener(delegate { OnClickPlay(); });
        btnReward.Init();
    }



    private void OnClickPlay()
    {

        InfoLevelBox.Setup().Show();
    }
}
