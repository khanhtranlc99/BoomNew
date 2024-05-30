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
    public GameObject[,] gridArray  = new GameObject[6,9];
    public GameObject[,] barrialArray = new GameObject[6, 9];
    public Transform parentTranform;
    public List<BarrierBase> lsBarrial;
    public List<GridBase> gridBasesId;


   
    public GridBase GridBase(int id)
    {
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            if(gridBasesId[i].id == id)
            {
                return gridBasesId[i];
            }
        }
        return null;
    }
    
    public void Awake()
    {
        Instance = this;
    }


    public void Init()
    {
        foreach(var item in lsBarrial)
        {
            item.Init();
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
    public int countSlime;
    public SlimeType slimeType;
}

[System.Serializable]
public class DataTime
{
    public float time;
}