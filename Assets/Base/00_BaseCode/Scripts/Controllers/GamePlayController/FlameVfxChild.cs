using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlameVfxChild : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public void HandleVfx()
    {
        spriteRenderer.DOFade(0.7f, 0.35f).OnComplete(delegate {
            spriteRenderer.DOFade(1, 0.35f).OnComplete(delegate {
               HandleVfx();
            });
        });
    }
}
