using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketVfx : MonoBehaviour
{
    public ParticleSystem flame;
    public SpriteRenderer spriteRenderer;
    public void HandleDisable()
    {
        flame.Play();
        spriteRenderer.color = new Color32(0, 0, 0,0);
        Invoke(nameof(OffRocket), 0.3f);
    }
    private void OffRocket()
    {
        SimplePool2.Despawn(this.gameObject);
    }

    private void OnDisable()
    {
        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

}
