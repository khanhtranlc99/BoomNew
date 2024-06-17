using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStreakController : MonoBehaviour
{
    public List<WinStreakGift> lsWinStreakGifts;
    public WinStreakGift GetWinStreakGift
    {
        get
        {
            return lsWinStreakGifts[UseProfile.WinStreak - 1];
        }
    }
    public WinStreakGift winStreak;
    public void Init ()
    {
        winStreak = new WinStreakGift();
        winStreak = GetWinStreakGift;
        
        if(winStreak != null)
        {
           
        }
    }



}
[System.Serializable]
public class WinStreakGift
{
    public int num;
    public GiftType giftType;

}