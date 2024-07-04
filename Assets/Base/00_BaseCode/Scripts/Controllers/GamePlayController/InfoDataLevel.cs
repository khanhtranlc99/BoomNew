using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class InfoDataLevel : MonoBehaviour
{
    public List<LevelData> levelDatas;
    public List<InfoData> lsInfoDatas;

    [Button]
    private void HandleFillData()
    {
        foreach(var item in levelDatas)
        {
            var temp = new InfoData();
            temp.difficult = temp.difficult;
            if (item.isSlimeLevel)
            {
                temp.isSlimeLevel = true;
                temp.conditionSlimes = item.conditionSlimes;
            }    
            else
            {
                temp.isSlimeLevel = true;
                temp.conditionSlimes = item.conditionSlimes;

            }    
           

            lsInfoDatas.Add(temp);

        }    
    }    
   
}
[System.Serializable]
 public class InfoData
{
    public Difficult difficult;
    public bool isSlimeLevel;
    public bool isTimeLevel;

    [ShowIf("isSlimeLevel", true)] public Data conditionSlimes;
    [ShowIf("isTimeLevel", true)] public DataTime conditionTime;
}