using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGame : MonoBehaviour
{
    [SerializeField]public long timeUpHeartGame = 30;
    public bool wasCoolDown;
    public float currentCoolDown;
    public void Init()
    {
        wasCoolDown = false;
        CheckHeart();


    }
 

    public void CheckHeart()
    {
        if(UseProfile.Heart < 5)
        {
            var secondsSinceLastUpdate = TimeManager.CaculateTime(UseProfile.TimeLastOverHealth, DateTime.Now);

            int minutesSinceLastUpdate = (int)(secondsSinceLastUpdate / 60);

          


            if (minutesSinceLastUpdate >= 10)
            {
                UpdateHeartBasedOnTime(minutesSinceLastUpdate);
                UseProfile.TimeLastOverHealth = DateTime.Now;
                float percent = (float)secondsSinceLastUpdate - minutesSinceLastUpdate;
                currentCoolDown = timeUpHeartGame- percent;
            }
            else
            {
                currentCoolDown = timeUpHeartGame- secondsSinceLastUpdate;
            }

        
            Debug.LogError("Min " + minutesSinceLastUpdate);
            Debug.LogError("currentCoolDown " + currentCoolDown);
            Debug.LogError("UseProfile.Heart " + UseProfile.Heart);

            wasCoolDown = true;
        }
   
       
    }

    void UpdateHeartBasedOnTime(int minutesPassed)
    {
        // Tính toán số lượng Heart cần tăng dựa trên số phút trôi qua
        int heartToAdd = minutesPassed / 10; // Mỗi 10 phút thêm 1 Heart

        // Giới hạn số lượng Heart không vượt quá 5
        if (UseProfile.Heart + heartToAdd > 5)
        {
            UseProfile.Heart = 5;
        }
        else
        {
            UseProfile.Heart += heartToAdd;
        }
    }
    public void HandleCoolDown()
    {
        if(UseProfile.Heart > 0)
        {
            UseProfile.Heart -= 1;
  
          
            var secondsSinceLastUpdate = TimeManager.CaculateTime(UseProfile.TimeLastOverHealth, DateTime.Now);
          
            if (secondsSinceLastUpdate > 0)
            {
                Debug.LogError("NoSave");
                return;
            }
            wasCoolDown = true;
            currentCoolDown = timeUpHeartGame;
            UseProfile.TimeLastOverHealth = DateTime.Now;
            Debug.LogError("Save");

        }    
    }    

    private void Update()
    {
 
        if (wasCoolDown)
        {
            if (UseProfile.Heart < 6)
            {
                currentCoolDown -=  Time.unscaledDeltaTime;
                if (currentCoolDown <= 0  )
                {
                    currentCoolDown = timeUpHeartGame;
                    UseProfile.Heart += 1;
                    UseProfile.TimeLastOverHealth = DateTime.Now;
                }
             
            }
            else
            {
                wasCoolDown = false;
           
            }

        }
    }
}
