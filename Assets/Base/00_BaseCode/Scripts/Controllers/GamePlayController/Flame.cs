using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
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
}
