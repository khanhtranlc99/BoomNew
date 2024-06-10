using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PackInShop : MonoBehaviour
{
    public TypePackIAP typePackIAP;
    public Button btnBuy;
    public Text tvBuy;
    public void Init()
    {
        //tvBuy.text = "" + ;
    
        tvBuy.text =  "" + GameController.Instance.iapController.GetPrice(this.typePackIAP);      
        btnBuy.onClick.AddListener(delegate { ButtonOnClick(); });
    
    }

    public void ButtonOnClick()
    {
        Debug.LogError("123132132132132_" + typePackIAP.ToString()) ;
        GameController.Instance.iapController.BuyProduct(typePackIAP);
    }
      
}
