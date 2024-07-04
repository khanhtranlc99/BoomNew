using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameVfx : MonoBehaviour
{
    public List<FlameVfxChild> lsSpriteRender;
    public List<ParticleSystem> lsParticleSystems;

    public void Init()
    {
        foreach(var item in lsSpriteRender)
        {
            item.HandleVfx();
        }
    }

    public void OnThunder()
    {
        foreach (var item in lsParticleSystems)
        {
            item.Play() ;
        }
    }    
}
