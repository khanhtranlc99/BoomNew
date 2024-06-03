using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class GridBase : MonoBehaviour
{
    public int id;
    public Transform post;
    public BarrierBase barrierBase;
    public List<GridBase> lsGridBase;
    public Vector2 vectorIJ;
    public SpriteRenderer spriteRenderer;
    public Sprite freeGrid;
    public Sprite normalGrid;
    public bool isFree;


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Grid")
    //    {
    //        lsGridBase.Add(collision.gameObject.GetComponent<GridBase>());
    //    }
    //}
    public GridBase GetNextGridForGhostSlime()
    {     
        return lsGridBase[Random.RandomRange(0, lsGridBase.Count)];
    }
    public GridBase GetNextGridForGhostSlime(List<int> idOlds)
    {
        foreach (var item in lsGridBase)
        {
            if (  !idOlds.Contains(item.id))
            {
                return item;
            }          
        }
        return null;
    }
    public GridBase GetNextGrid()
    {
        foreach(var item in lsGridBase)
        {
            if(item.barrierBase == null)
            {
                return item;
            }
            if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Slime)
            {
                return item;
            }

            if (item.barrierBase != null &&  item.barrierBase.barrierType == BarrierType.Boom)
            {
                return item;
            }
            if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.ComeThrough  )
            {        
                return item;
            }
        }
        return null;
    }
  
    public GridBase GetNextGrid(  List<int> idOlds)
    {
        foreach (var item in lsGridBase)
        {
            if (item.barrierBase == null && !idOlds.Contains(item.id))
            {
                return item;
            }
            if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.ComeThrough && !idOlds.Contains(item.id))
            {
                return item;
            }
        }
        return null;
    }

    public void Save()
    {
        var lsid = new List<int>();
        foreach (var item in lsGridBase)
        {
            lsid.Add(item.id);
        }

        var temp = JsonConvert.SerializeObject(lsid);
        PlayerPrefs.SetString("GridBase" + id.ToString(), temp);
    }
    public void Load()
    {
        var data = PlayerPrefs.GetString("GridBase" + id.ToString());
        var tempLoad = JsonConvert.DeserializeObject<List<int>>(data);
        foreach (var item in tempLoad)
        {

            lsGridBase.Add(LevelData.Instance.GridBase(item));
        }
    }
    
    public void HandleVectorIJ()
    {
        var temp_0 = new Vector2(vectorIJ.x + 1, vectorIJ.y);
        var temp_1 = new Vector2(vectorIJ.x - 1, vectorIJ.y);
        var temp_2 = new Vector2(vectorIJ.x  , vectorIJ.y + 1);
        var temp_3 = new Vector2(vectorIJ.x  , vectorIJ.y - 1);

      var  testlsGridBase = new List<GridBase>();
        foreach(var item in lsGridBase)
        {
            if(item.vectorIJ == temp_0 || item.vectorIJ == temp_1 || item.vectorIJ == temp_2 || item.vectorIJ == temp_3)
            {
                testlsGridBase.Add(item); ;
            }
        }
        lsGridBase.Clear();
        lsGridBase = new List<GridBase>();
        foreach(var item in testlsGridBase)
        {
            lsGridBase.Add(item);
        }

    }

    public void HandleFreeGrid()
    {
        isFree = true;
        spriteRenderer.sprite = freeGrid;
    }

    [Button]
    private void HandleTools()
    {
        foreach(var item in lsGridBase)
        {
            if(item == null)
            {
                lsGridBase.Remove(item);
            }
        }
    }
}
