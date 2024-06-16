using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopCoinBox : BaseBox
{
    public static ShopCoinBox _instance;
    public static ShopCoinBox Setup()
    {
        if (_instance == null)
        {
            _instance = Instantiate(Resources.Load<ShopCoinBox>(PathPrefabs.SHOP_COIN_BOX));
            _instance.Init();
        }
        _instance.InitState();
        return _instance;
    }

    public List<PackInShop> lsPackBase;
    public Button btnClose;

    public void Init()
    {
        foreach(var item in lsPackBase)
        {
            item.Init();
        }
        btnClose.onClick.AddListener(Close);
    }
    public void InitState()
    {

    }
}
