using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandle : MonoBehaviour
{
    public void HandleCount()
    {
        GamePlayController.Instance.playerContain.winStreakController.HandleWinStreak();
    }
}
