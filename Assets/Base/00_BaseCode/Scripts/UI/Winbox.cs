using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    public CanvasGroup canvasGroup;
    public void Init()
    {

    }   
    public void InitState()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 3);

    }    
}
