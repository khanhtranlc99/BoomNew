using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class QuestController : MonoBehaviour
{
    public static int IdCurrentQuest
    {
        get
        {
            return PlayerPrefs.GetInt("CurrentQuest", -1);
        }
        set
        {
            PlayerPrefs.SetInt("CurrentQuest", value);
            PlayerPrefs.Save();
        }
    }
    public static int ProgessQuest
    {
        get
        {
            return PlayerPrefs.GetInt("ProgessQuest", 0);
        }
        set
        {
            PlayerPrefs.SetInt("ProgessQuest", value);
            PlayerPrefs.Save();
        }
    }
    public List<QuestData> lsQuestData;
    public Quest currentQuest;


    private Quest GetQuest
    {
        get
        {
            float temp = Random.Range(0, 100);
            foreach(var item in lsQuestData)
            {
                if(temp >= item.minPercent && temp < item.maxPercent)
                {
                    return item.lsQuest[Random.Range(0, item.lsQuest.Count)];
                }
            }
            return null;
        }
    }
    private Quest GetQuestById(int id)
    {
         
       foreach(var item in lsQuestData)
        {
           foreach(var quest in item.lsQuest)
            {
                if(quest.idQuest == id)
                {
                    return quest;
                }
            }
        }
        return null;
     
    }



    public void Init()
    {
       if(IdCurrentQuest == -1)
        {
            ResetQuest();
        }
        else
        {
            currentQuest = new Quest();
            currentQuest = GetQuestById(IdCurrentQuest);
             
        }
    }
    public void HandleCheckCompleteQuest(QuestTargetType targetType)
    {
        if(currentQuest.targetType == targetType)
        {
            if(ProgessQuest < currentQuest.numTarget)
            {
                ProgessQuest += 1;
            }
        } 
    }

    public void ResetQuest()
    {
        currentQuest = new Quest();
        currentQuest = GetQuest;
        IdCurrentQuest = currentQuest.idQuest;
        ProgessQuest = 0;
    }

    

}
[System.Serializable]
public class QuestData
{
    public List<Quest> lsQuest;
    public float minPercent;
    public float maxPercent;
    public Quest GetRandomQuest
    {
        get
        {
            return lsQuest[Random.RandomRange(0, lsQuest.Count)];
        }
    }
}


[System.Serializable]
public class Quest
{
    public int idQuest;
    public GiftType giftType;
    public QuestTargetType targetType;
    public int numGift;
    public int numTarget;

}



public enum QuestTargetType
{
    Bloom,
    Tree,
    Brick,
    Rock,
    WoodenBox,
    Chest,
    Slime_RED,
    Slime_BLUE,
    Slime_IRON,
    Slime_VIOLET,
    Slime_GHOST,
    Slime_ICE,
    Slime_FLASH
}