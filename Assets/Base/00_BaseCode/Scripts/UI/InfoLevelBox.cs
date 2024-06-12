using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InfoLevelBox : BaseBox
{
    public static InfoLevelBox _instance;
    public static InfoLevelBox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<InfoLevelBox>(PathPrefabs.INFO_LEVEL_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnClose;
    public void Init()
    {
        btnPlay.onClick.AddListener(delegate { SceneManager.LoadScene("GamePlay"); });
        btnClose.onClick.AddListener(delegate { Close(); });
    }
    public void InitState()
    {

    }
}
