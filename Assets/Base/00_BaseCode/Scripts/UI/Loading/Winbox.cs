using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Winbox : BaseBox
{
    public static Winbox _instance;
    public static Winbox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<Winbox>(PathPrefabs.WIN_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }

    public Button nextButton;
    public Button rewardButton;
    public Text tvScore;
    public Text tvCoin;

    public void Init()
    {
        nextButton.onClick.AddListener(delegate { UseProfile.CurrentLevel += 1; SceneManager.LoadScene("GamePlay"); });
    }   
    public void InitState()
    {
       
     


    }    
    


}
