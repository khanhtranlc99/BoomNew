using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketVfx : MonoBehaviour
{
    public ParticleSystem flame;
 
    public void HandleDisable()
    {
        //flame.Play();
 
        Invoke(nameof(OffRocket), 0.3f);
    }
    private void OffRocket()
    {
        SimplePool2.Despawn(this.gameObject);
    }

    private void OnDisable()
    {
      
    }

}
