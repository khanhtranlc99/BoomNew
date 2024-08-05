using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoosterPackBox : BaseBox
{
    public static BoosterPackBox _instance;
    public static BoosterPackBox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<BoosterPackBox>(PathPrefabs.BOOSTER_PACK_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }

    public Button btnClose;

    public PackInShop iapPack;


    public void Init()
    {
        btnClose.onClick.AddListener(Close);
        iapPack.Init();
    }
    public void InitState()
    {

    }
}
