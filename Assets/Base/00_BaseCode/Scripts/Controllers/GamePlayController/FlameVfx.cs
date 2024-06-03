using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameVfx : MonoBehaviour
{
    public List<FlameVfxChild> lsSpriteRender;


    public void Init()
    {
        foreach(var item in lsSpriteRender)
        {
            item.HandleVfx();
        }
    }
}
