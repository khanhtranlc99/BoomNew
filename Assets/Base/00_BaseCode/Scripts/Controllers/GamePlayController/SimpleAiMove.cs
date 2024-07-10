using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SimpleAiMove : MonoBehaviour
{
    public List<Transform> lsTranform;
    private void Start()
    {
        Move();
    }
    public void Move()
    {
        var temp = lsTranform[Random.RandomRange(0, lsTranform.Count)];
        if (this.transform.position.x < temp.position.x)
        {
            this.transform.localScale = new Vector3( -2, 2, 2);
        }
        else
        {
            this.transform.localScale = new Vector3(2, 2, 2);
        }
        this.transform.DOMove(temp.position, Random.RandomRange(4,6)).OnComplete(delegate { Move(); });
    }
}
