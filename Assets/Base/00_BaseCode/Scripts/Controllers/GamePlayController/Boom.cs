using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boom : BarrierBase
{
    public Flame flame;
    public float spacing = 0;
    public List<GridBase> leftGridbase;
    public List<GridBase> rightGridbase;
    public List<GridBase> upGridbase;
    public List<GridBase> downGridbase;

    public int countUp;
    public int countDown;
    public int countRight;
    public int countLeft;

    public AudioClip boom;

    public Sprite flame_1;
    public Sprite flame_2;
    public Sprite flame_3;
    public void HandleExplosion()
    {


        GameController.Instance.musicManager.PlayOneShot(boom);
        SpawnCross(1 + UseProfile.FlameUp_Item);

        gridBase.barrierBase = null;
        SimplePool2.Despawn(this.gameObject);

    }

    void SpawnCross(int units)
    {
        // Vị trí trung tâm
        Vector3 centerPosition = transform.position;

        // Tạo phần tử ở trung tâm
      var tempMid =  SimplePool2.Spawn(flame, centerPosition, Quaternion.identity);
        tempMid.Init(flame_2, FlameType.Mid);
        countUp = units;
        countDown = units;
        countRight = units;
        countLeft = units;
        if (countUp > upGridbase.Count)
        {
            countUp = upGridbase.Count;
        }
        if (countDown > downGridbase.Count)
        {
            countDown = downGridbase.Count;
        }
        if (countRight > rightGridbase.Count)
        {
            countRight = rightGridbase.Count;
        }
        if (countLeft > leftGridbase.Count)
        {
            countLeft = leftGridbase.Count;
        }

        if (countUp > 0)
        {
            for (int i = 1; i <= countUp; i++)
            {
                // Tạo phần tử phía trên
                Vector3 upPosition = centerPosition + new Vector3(0, spacing, 0) * i;
             var tempUp =   SimplePool2.Spawn(flame, upPosition, Quaternion.identity);
                if(i == countUp)
                {
                    tempUp.Init(flame_3, FlameType.Up);
                }
                else
                {
                    tempUp.Init(flame_1, FlameType.Up);
                }
      
            }
        }

        if (countDown > 0)
        {
            for (int i = 1; i <= countDown; i++)
            {
                // Tạo phần tử phía dưới
                Vector3 downPosition = centerPosition + new Vector3(0, -spacing, 0) * i;
                var tempDown = SimplePool2.Spawn(flame, downPosition, Quaternion.identity);
                if(i == countDown)
                {
                    tempDown.Init(flame_3, FlameType.Down);
                }
                else
                {
                    tempDown.Init(flame_1, FlameType.Down);
                }
           
            }
        }
        if (countLeft > 0)
        {
            for (int i = 1; i <= countLeft; i++)
            {
                // Tạo phần tử phía trái
                Vector3 leftPosition = centerPosition + new Vector3(-spacing, 0, 0) * i;
                var tempLeft = SimplePool2.Spawn(flame, leftPosition, Quaternion.identity);
                if(i == countLeft)
                {
                    tempLeft.Init(flame_3, FlameType.Left);
                }
                else
                {
                    tempLeft.Init(flame_1, FlameType.Left);
                }
        
            }
        }
        if (countRight > 0)
        {
            for (int i = 1; i <= countRight; i++)
            {
                // Tạo phần tử phía phải
                Vector3 rightPosition = centerPosition + new Vector3(spacing, 0, 0) * i;
                var tempRight = SimplePool2.Spawn(flame, rightPosition, Quaternion.identity);
          
                if (i == countRight)
                {
                    tempRight.Init(flame_3, FlameType.Right);
                }
                else
                {
                    tempRight.Init(flame_1, FlameType.Right);
                }
            }
        }

    }


    private void OnDisable()
    {
        leftGridbase.Clear();
        rightGridbase.Clear();
        upGridbase.Clear();
        downGridbase.Clear();
        this.transform.position = new Vector3(-5, 0, 0);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    public override void Init()
    {

        leftGridbase.Clear();
        rightGridbase.Clear();
        upGridbase.Clear();
        downGridbase.Clear();
        countUp = 0;
        countDown = 0;
        countRight = 0;
        countLeft = 0;
        if (gridBase.leftGridBase.Count > 0)
        {
            foreach (var item in gridBase.leftGridBase)
            {
                if (item.barrierBase == null)
                {
                    leftGridbase.Add(item);
                }      
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.ComeThrough)
                {
                    leftGridbase.Add(item);
                    break;
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Slime)
                {
                    leftGridbase.Add(item);
                    break;
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Block)
                {
                    leftGridbase.Add(item);
                    break;
                }
            }
        }
        if (gridBase.rightGridBase.Count > 0)
        {
            foreach (var item in gridBase.rightGridBase)
            {
                if (item.barrierBase == null)
                {
                    rightGridbase.Add(item);
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.ComeThrough)
                {
                    rightGridbase.Add(item);
                    break;
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Slime)
                {
                    rightGridbase.Add(item);
                    break;
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Block)
                {
                    rightGridbase.Add(item);
                    break;
                }
            }
        }
        if (gridBase.downGridBase.Count > 0)
        {
            foreach (var item in gridBase.downGridBase)
            {
                if (item.barrierBase == null)
                {
                    downGridbase.Add(item);
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.ComeThrough)
                {
                    downGridbase.Add(item);
                    break;
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Slime)
                {
                    downGridbase.Add(item);
                    break;
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Block)
                {
                    downGridbase.Add(item);
                    break;
                }
            }
        }
        if (gridBase.upGridBase.Count > 0)
        {
            foreach (var item in gridBase.upGridBase)
            {
                if (item.barrierBase == null)
                {
                    upGridbase.Add(item);
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.ComeThrough)
                {
                    upGridbase.Add(item);
                    break;
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Slime)
                {
                    upGridbase.Add(item);
                    break;
                }
                if (item.barrierBase != null && item.barrierBase.barrierType == BarrierType.Block)
                {
                    upGridbase.Add(item);
                    break;
                }
            }
        }
    }

    public override void TakeDame()
    {

    }
}
