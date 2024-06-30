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
    public GameObject ice;
    public List<GridBase> leftGridBase;
    public List<GridBase> rightGridBase;
    public List<GridBase> upGridBase;
    public List<GridBase> downGridBase;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Grid")
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
    public GridBase GetNextGrid(bool slimeFind)
    {
        foreach (var item in lsGridBase)
        {
            if (item.barrierBase == null)
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
            if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.ComeThrough  && !idOlds.Contains(item.id))
            {
                return item;
            }
        }
        return null;
    }

    public GridBase GetRandomGrid
    {
        get
        {
            return lsGridBase[Random.RandomRange(0, lsGridBase.Count)];
        }
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

    public GridBase GetGrid(MoveBlockType moveBlockType)
    {
        var temp_0 = new Vector2( );
        switch (moveBlockType)
        {
            case MoveBlockType.Up:
                temp_0 = new Vector2(vectorIJ.x, vectorIJ.y + 1);
                break;
            case MoveBlockType.Down:
                temp_0 = new Vector2(vectorIJ.x, vectorIJ.y - 1);
                break;
            case MoveBlockType.Left:
                temp_0 = new Vector2(vectorIJ.x - 1, vectorIJ.y);
                break;
            case MoveBlockType.Right:
                temp_0 = new Vector2(vectorIJ.x + 1, vectorIJ.y);
                break;

        }

        foreach(var item in lsGridBase)
        {
            if(item.vectorIJ == temp_0)
            {
                return item;
            }
        }

        return null;



    }
    [Button]
    public void Test( )
    {
        ShootRaycastsFromBoom(this.transform.position);
    }

    void ShootRaycastsFromBoom(Vector2 boomPosition)
    {
        Vector2 RightDirection = Vector2.right;

        // Lấy tất cả các collider mà raycast đi qua
        RaycastHit2D[] hits = Physics2D.RaycastAll(boomPosition, RightDirection, 10); // Độ dài của raycast là 10

        // Vẽ raycast bằng màu đỏ
        Debug.DrawRay(boomPosition, RightDirection * 10, Color.red, 10); // Vẽ raycast với độ dài 10 và hiển thị trong 10 giây

        foreach (var hit in hits)
        {
            // Xử lý khi raycast chạm vào đối tượng
            if (hit.collider.gameObject.GetComponent<GridBase>() != null && hit.collider.gameObject.GetComponent<GridBase>() != this)
            {
                rightGridBase.Add(hit.collider.gameObject.GetComponent<GridBase>());
            }
        }

        Vector2 UpDirection = Vector2.up;

        // Lấy tất cả các collider mà raycast đi qua
        RaycastHit2D[] hits2 = Physics2D.RaycastAll(boomPosition, UpDirection, 10); // Độ dài của raycast là 10

        // Vẽ raycast bằng màu đỏ
        Debug.DrawRay(boomPosition, UpDirection * 10, Color.red, 10); // Vẽ raycast với độ dài 10 và hiển thị trong 10 giây

        foreach (var hit in hits2)
        {
            // Xử lý khi raycast chạm vào đối tượng
            if (hit.collider.gameObject.GetComponent<GridBase>() != null && hit.collider.gameObject.GetComponent<GridBase>() != this)
            {
                upGridBase.Add(hit.collider.gameObject.GetComponent<GridBase>());
            }
        }

        Vector2 LeftDirection = Vector2.left;

        // Lấy tất cả các collider mà raycast đi qua
        RaycastHit2D[] hits3 = Physics2D.RaycastAll(boomPosition, LeftDirection, 10); // Độ dài của raycast là 10

        // Vẽ raycast bằng màu đỏ
        Debug.DrawRay(boomPosition, LeftDirection * 10, Color.red, 10); // Vẽ raycast với độ dài 10 và hiển thị trong 10 giây

        foreach (var hit in hits3)
        {
            // Xử lý khi raycast chạm vào đối tượng
            if (hit.collider.gameObject.GetComponent<GridBase>() != null && hit.collider.gameObject.GetComponent<GridBase>() != this)
            {
                leftGridBase.Add(hit.collider.gameObject.GetComponent<GridBase>());
            }
        }

        Vector2 DownDirection = Vector2.down;

        // Lấy tất cả các collider mà raycast đi qua
        RaycastHit2D[] hits4 = Physics2D.RaycastAll(boomPosition, DownDirection, 10); // Độ dài của raycast là 10

        // Vẽ raycast bằng màu đỏ
        Debug.DrawRay(boomPosition, DownDirection * 10, Color.red, 10); // Vẽ raycast với độ dài 10 và hiển thị trong 10 giây

        foreach (var hit in hits4)
        {
            // Xử lý khi raycast chạm vào đối tượng
            if (hit.collider.gameObject.GetComponent<GridBase>() != null && hit.collider.gameObject.GetComponent<GridBase>() != this)
            {
                downGridBase.Add(hit.collider.gameObject.GetComponent<GridBase>());
            }
        }
    }



    [Button]
    public void HandleFreeGrid()
    {
        isFree = true;
        ice.gameObject.SetActive(true);
 
    }
    [Button]
    public void HandleUnFreeze()
    {
        isFree = false;
        ice.gameObject.SetActive(false);
  
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
