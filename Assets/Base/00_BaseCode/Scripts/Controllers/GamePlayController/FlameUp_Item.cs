using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlameUp_Item : MonoBehaviour
{
    public int num;
    public Text tvNum;
    public GameObject icon;

    public void Init()
    {
        if(UseProfile.FlameUp_Item > 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.FlameUp_Item;
        }
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.FLAMEUP_ITEM, HandleShowFlameUp);
    }

    public void HandleShowFlameUp(object param)
    {
        if (UseProfile.FlameUp_Item > 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.FlameUp_Item;
        }
    }
    private void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.FLAMEUP_ITEM, HandleShowFlameUp);
    }
}
