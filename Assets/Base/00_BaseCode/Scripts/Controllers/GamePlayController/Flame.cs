using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum FlameType
{
    Up,
    Down,
    Left,
    Right,
        Mid,
}
public class Flame : MonoBehaviour
{
    
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private void OnEnable()
    {
        StartCoroutine(Stop());
    }
   
    public IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.4f);
        if (!GamePlayController.Instance.playerContain.levelData.isSlimeTakeDame)
        {
            GamePlayController.Instance.HandleCheckLose();
        }
     
   
        SimplePool2.Despawn(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Barrial")
        {
            spriteRenderer.sortingOrder = 1;
            collision.gameObject.GetComponent<BarrierBase>().TakeDame();
        }
        if (collision.gameObject.tag == "Grid")
        {
           if (collision.gameObject.GetComponent<GridBase>().isFree)
            {
                collision.gameObject.GetComponent<GridBase>().HandleUnFreeze();
            }
        }
    }
    public void Init (RuntimeAnimatorController param, FlameType flameType )
    {
        animator.runtimeAnimatorController = param;
        //spriteRenderer.color = new Color32(0, 0, 0, 0);
        //spriteRenderer.DOColor(new Color32(255, 255, 255, 255), 0.2f).SetEase(Ease.Flash);
        switch (flameType)
        {
            case FlameType.Up:
                this.transform.localEulerAngles = new Vector3(0, 0, 90);
                break;
            case FlameType.Down:
                this.transform.localEulerAngles = new Vector3(0, 0, 90);

                this.transform.localScale = new Vector3(-1, 1, 1);
                break;

            case FlameType.Left:
                this.transform.localScale = new Vector3(-1, 1, 1);
                break;

            case FlameType.Right:

                break;

            case FlameType.Mid:

                break;
        }
    }
    private void OnDisable()
    {
        spriteRenderer.sortingOrder = 3;
        this.transform.localEulerAngles = new Vector3(0, 0, 0);
        this.transform.localScale = new Vector3(1, 1, 1);
    }
    private void OnDestroy()
    {
        spriteRenderer.DOKill();
    }
}
