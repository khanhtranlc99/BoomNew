using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public enum Difficult
{
    Normal,
    Hard,
    VeryHard

}

public class LevelData : SerializedMonoBehaviour
{
    public Difficult difficult;
    public bool isSlimeLevel;
    public bool isTimeLevel;

    [ShowIf("isSlimeLevel", true)] public Data conditionSlimes;
    [ShowIf("isTimeLevel", true)] public DataTime conditionTime;

    public int boomLimit;
    public static LevelData Instance ;
    public int row  ;
    public int cow ;
    public GameObject[,] gridArray = new GameObject[6, 9];
    public GameObject[,] gridArrayCurrent = new GameObject[6, 9];
    public GameObject[,] barrialArray = new GameObject[6, 9];
    public Transform parentGrid;
    public Transform parentTranform;
 
    public List<BarrierBase> lsSmiles;
    public List<BarrierBase> lsBloomSoms;
    public List<GridBase> gridBasesId;
    public Transform postLeft;
    public Transform postRight;
    
    private void OnDrawGizmos()
    {
        Instance = this;
    }
   
    public GridBase GridBase(int id)
    {
        Debug.LogError("GridBasesId.Count_" + gridBasesId.Count);
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            if (gridBasesId[i].id == id)
            {
                return gridBasesId[i];
            }
        }
        return null;
    }
    public GridBase GridBaseNull()
    {
        Debug.LogError("GridBasesId.Count_" + gridBasesId.Count);
        for (int i = 0; i < gridBasesId.Count; i++)
        {
           if(gridBasesId[i].barrierBase == null)
            {
                return gridBasesId[i];
            }
        }
        return null;
    }
    public BarrierBase GetRandomSlimeForRocketBooster
    {
        get
        {
            var temp = new List<BarrierBase>();
            foreach (var item in lsSmiles)
            {
                if (item != null && item.Hp > 0)
                {
                    temp.Add(item);
                }
            }
            return temp[Random.Range(0, temp.Count)];
        }   
    }

    public bool isSlimeTakeDame
    {
        get
        {
            foreach(var item in lsSmiles)
            {
                if(item != null)
                {
                    if(item.GetComponent<SlimeBase>().wasTakeDame)
                    {
                        return true;
                    }    
                }    
            }
            return false;
        }
    }    

    public void Init()
    {
        ShuffleBase();

        Invoke(nameof(InitBase),2);
      
      
   
    }
    public void Init (bool noWait)
    {
        ShuffleBase();
        InitBase();
    }    
    void InitBase()
    {
        foreach (var item in lsSmiles)
        {
            item.Init();
        }
        if (lsBloomSoms.Count > 0)
        {
            foreach (var item in lsBloomSoms)
            {
                item.Init();
            }
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
    private void HandleSpawnGrid()
    {
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                if (gridArray[i, j] != null)
                {
                    float scaleFactor = 0.7f;

                    var temp = Instantiate(gridArray[i, j], new Vector3(i, j) * scaleFactor, Quaternion.identity);
                    temp.transform.parent = parentGrid;
                    temp.GetComponent<GridBase>().vectorIJ = new Vector2(i, j);
                    gridArrayCurrent[i, j] = temp;
                    gridBasesId.Add(temp.gameObject.GetComponent<GridBase>());
                }
            }
        }
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            gridBasesId[i].id = i;
        }

    }
#if UNITY_EDITOR
    [Button]
    public void SpawnBarrial()
    {
        for(int i = 0; i < barrialArray.GetLength(0); i++)
        {
           for (int j = 0; j < barrialArray.GetLength(1); j++)
            {
                if(barrialArray[i,j] != null)
                {
                    //GameObject spawnedObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
      
                    var temp = (GameObject)PrefabUtility.InstantiatePrefab(barrialArray[i, j].gameObject);
                    temp.transform.position = gridArrayCurrent[i, j].transform.position;
             
                    temp.GetComponent<BarrierBase>().gridBase = gridArrayCurrent[i, j].GetComponent<GridBase>();
                    gridArrayCurrent[i, j].GetComponent<GridBase>().barrierBase = temp.GetComponent<BarrierBase>();
                    temp.transform.parent = parentTranform;
                    if(temp.gameObject.GetComponent<SlimeBase>() != null)
                    {
                        lsSmiles.Add(temp.gameObject.GetComponent<SlimeBase>());                   
                    }
                    if (temp.gameObject.GetComponent<Bloomsom>() != null)
                    {
                        lsBloomSoms.Add(temp.gameObject.GetComponent<Bloomsom>());
                    }
                }

            }
        }
    }
#endif

    public void HandleFreezeBooster()
    {
        //foreach (var item in lsSmiles)
        //{
        //    if(item != null)
        //    {
        //        item.gameObject.GetComponent<SlimeBase>().HandlePause();
        //    }

        //}
        StartCoroutine(StopFreeze());
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.FREEZE);
    }

    private IEnumerator StopFreeze()
    {
        yield return new WaitForSeconds(5);
        GamePlayController.Instance.playerContain.freeze_Booster.OffVfx();
    }

    public void Pause ()
    {
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.PAUSE);
    }
    public void StopPause()
    {
        EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.STOPPAUSE);
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
        Debug.LogError("save");
    }
    [Button]
    private void Load()
    {
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            gridBasesId[i].Load();
        }
        TestLoadVecIJ();
        Debug.LogError("load");
    }

    [Button]
    private void TestLoadVecIJ()
    {
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            gridBasesId[i].HandleVectorIJ();
        }
    }
    [Button]
    private void ClearBarrial()
    {
        for (int i = 0; i < barrialArray.GetLength(0); i++)
        {
            for (int j = 0; j < barrialArray.GetLength(1); j++)
            {
                barrialArray[i, j] = null;
                
            }
        }
     
        lsBloomSoms.Clear();
        lsSmiles.Clear();
        Debug.LogError("clear");
    }
 
    private void ShuffleBase()
    {
        for (int i = 0; i < gridBasesId.Count; i++)
        {
            gridBasesId[i].lsGridBase.Shuffle();
        }
    }
 
    [Button]
    private void Test()
    {
        gridArray = new GameObject[row, cow];
        gridArrayCurrent = new GameObject[row, cow];
        barrialArray = new GameObject[row, cow];
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