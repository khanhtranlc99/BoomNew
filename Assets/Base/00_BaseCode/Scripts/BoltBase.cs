using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine.UI;
public class BoltBase : MonoBehaviour
{
    public List<SkeletonAnimation> skeletonAnimation;
    public Text tvState;
    public void Start()
    {
   
    }
    [Button]
    private void PlayIdle()
    {
        foreach (var item in skeletonAnimation)
        {
            item.SetAnimation("idle", true);
        }
        tvState.text = "idle";
    }
    [Button]
    private void PlayMove()
    {
        foreach (var item in skeletonAnimation)
        {
            item.SetAnimation("move", true);
        }
        tvState.text = "move";
    }
    [Button]
    private void PlayDie()
    {
        foreach (var item in skeletonAnimation)
        {
            item.SetAnimation("die", true);
        }
        tvState.text = "die";
    }
}
