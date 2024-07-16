using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeartBarSlime : MonoBehaviour
{
    public List<HeartSlime> lsHeartSlime;
    HeartSlime current;
    public void Init()
    {
        foreach(var item in lsHeartSlime)
        {
            item.heart.SetActive(true);
        }
        Invoke(nameof(HandleOffHeart),1.5f);
    }
    private void HandleOffHeart()
    {
        foreach (var item in lsHeartSlime)
        {
            item.heart.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(0, 0, 0, 0), 0.3f);
            item.spriteRendererHeart.DOColor(new Color32(0, 0, 0, 0), 0.3f);
        }
    }

    public void HandleSupTrackHeart()
    {
        foreach (var item in lsHeartSlime)
        {
            item.heart.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            item.spriteRendererHeart.color = new Color32(255, 255, 255, 255);
        }

        for (int i = lsHeartSlime.Count - 1; i >= 0; i--)
        {
          if(lsHeartSlime[i].spriteRendererHeart.gameObject.activeSelf)
            {
                current = lsHeartSlime[i];
                break;
            }
        }
         

        current.spriteRendererHeart.DOColor(new Color32(0,0,0,0),0.7f).OnComplete(delegate {
            current.spriteRendererHeart.gameObject.SetActive(false);
            HandleOffHeart();
       
        });
    }
    
    private void OnDestroy()
    {
        foreach (var item in lsHeartSlime)
        {
            item.spriteRendererHeart.DOKill();
            item.heart.gameObject.GetComponent<SpriteRenderer>().DOKill();
        }
    }
}
[System.Serializable]
public class HeartSlime
{
    public GameObject heart;
    public SpriteRenderer spriteRendererHeart;
}