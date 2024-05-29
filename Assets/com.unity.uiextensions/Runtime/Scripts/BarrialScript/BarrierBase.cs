using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarrierType
{
    Block,
    ComeThrough,
    Boom
}
public abstract class BarrierBase : MonoBehaviour
{
 
   
    public SpriteRenderer spriteRenderer;
    public BarrierType barrierType;
     public GridBase gridBase;
    public bool wasTakeDame = false;
    public int Hp;
    public abstract void Init();
    public abstract void TakeDame();
    


}