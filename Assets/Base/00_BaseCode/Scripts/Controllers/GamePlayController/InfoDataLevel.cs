using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class InfoDataLevel : MonoBehaviour
{
    public List<LevelData> levelDatas;
    public List<InfoData> lsInfoDatas;
    public List<DataPrefabSlimeUI> lsDataPrefabSlimeUIs;
    public DataPrefabSlimeUI GetDataPrefabSlimeUI(SlimeType slime)
    {
        foreach(var item in lsDataPrefabSlimeUIs)
        {
            if(item.slimeType == slime)
            {
                return item;
            }    
        }
        return null;
    }    
    public List<Transform> lsDataTranform;
    public GameObject tranformAI;

    public void Init ()
    {
       
        foreach(var item in lsInfoDatas[UseProfile.CurrentLevel - 1].conditionSlimes.lsDataSlime)
        {
            var temp = Instantiate( GetDataPrefabSlimeUI(item.slimeType).aiMove);
            temp.transform.parent = tranformAI.transform;
            temp.transform.localScale = new Vector3(1, 1, 1);
            temp.Init(lsDataTranform);
        }    
    }  

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
[System.Serializable]
public class DataPrefabSlimeUI
{
    public SimpleAiMove aiMove;
    public SlimeType slimeType;
}    