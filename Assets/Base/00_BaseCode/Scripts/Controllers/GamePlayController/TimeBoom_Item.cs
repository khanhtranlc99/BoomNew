using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeBoom_Item : MonoBehaviour
{

    public int num;
    public Text tvNum;
    public GameObject icon;

    public void Init()
    {
        if (UseProfile.TimeBoom_Item > 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.TimeBoom_Item;
        }
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.TIMEBOOM_ITEM, HandleShowFlameUp);
    }

    public void HandleShowFlameUp(object param)
    {
        if (UseProfile.TimeBoom_Item > 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.TimeBoom_Item;
        }
    }
    private void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.TIMEBOOM_ITEM, HandleShowFlameUp);
    }
}
