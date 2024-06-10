using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FastBoom_Item : MonoBehaviour
{
    public int num;
    public Text tvNum;
    public GameObject icon;

    public void Init()
    {
        if (UseProfile.FastBoom_Item > 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.FastBoom_Item;
        }
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.FASTBOOM_ITEM, HandleShowFlameUp);
    }

    public void HandleShowFlameUp(object param)
    {
        if (UseProfile.FastBoom_Item > 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.FastBoom_Item;
        }
    }
    private void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.FASTBOOM_ITEM, HandleShowFlameUp);
    }
}
