using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class LevelData : SerializedMonoBehaviour
{
    public bool isSlimeLevel;
    public bool isTimeLevel;

    [ShowIf("isSlimeLevel", true)] public Data conditionSlimes;
    [ShowIf("isTimeLevel", true)] public DataTime conditionTime;

    public int boomLimit;
    public static LevelData Instance;
    public GameObject[,] gridArray = new GameObject[6, 9];
    public GameObject[,] barrialArray = new GameObject[6, 9];
    public Transform parentTranform;
    public List<BarrierBase> lsSmiles;
    public List<GridBase> gridBasesId;



    public GridBase GridBase(int id)
    {
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            if (gridBasesId[i].id == id)
            {
                return gridBasesId[i];
            }
        }
        return null;
    }
    public BarrierBase GetRandomSlime
    {
        get
        {
            var temp = new List<BarrierBase>();
            foreach (var item in lsSmiles)
            {
                if (item != null)
                {
                    temp.Add(item);
                }
            }
            return temp[Random.Range(0, temp.Count)];
        }   
    }



    public void Init()
    {
        foreach (var item in lsSmiles)
        {
            item.Init();
        }
    }
    public bool isAllSlimeDie
    {
        get
        {
            foreach (var item in lsSmiles)
            {
                if (item != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
  
    

    [Button]
    public void SpawnBarrial()
    {
        for(int i = 0; i < barrialArray.GetLength(0); i++)
        {
           for (int j = 0; j < barrialArray.GetLength(1); j++)
            {
                if(barrialArray[i,j] != null)
                {
                   var temp = Instantiate(barrialArray[i, j].gameObject, gridArray[i, j].transform.position, Quaternion.identity);
                    temp.GetComponent<BarrierBase>().gridBase = gridArray[i, j].GetComponent<GridBase>();
                    gridArray[i, j].GetComponent<GridBase>().barrierBase = temp.GetComponent<BarrierBase>();
                    temp.transform.parent = parentTranform;
                }

            }
        }
    }
    public void HandleFreezeBooster()
    {
        //foreach (var item in lsSmiles)
        //{
        //    if(item != null)
        //    {
        //        item.gameObject.GetComponent<SlimeBase>().HandlePause();
        //    }

        //}
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.FREEZE);
    }

    //[Button]
    //private void HandleFillIdGrid()
    //{
    //    for (int i = 0; i < gridBasesId.Count; i++)
    //    {
    //        gridBasesId[i].id = i;
    //    }
    //}

    [Button]
    private void Save()
    {
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            gridBasesId[i].Save();
        }
    }
    [Button]
    private void Load()
    {
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            gridBasesId[i].Load();
        }
    }
    [Button]
    private void LoadVecIJ()
    {
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                gridArray[i, j].gameObject.GetComponent<GridBase>().vectorIJ = new Vector2(i, j);

            }
        }
    }
    [Button]
    private void TestLoadVecIJ()
    {
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            gridBasesId[i].HandleVectorIJ();
        }
    }
    
}

[System.Serializable]
public class Data
{
    public List<DataSlime> lsDataSlime;

}

[System.Serializable]
public class DataSlime
{
    public SlimeType slimeType;
    public int countSlime;

}

[System.Serializable]
public class DataTime
{
    public float time;
}