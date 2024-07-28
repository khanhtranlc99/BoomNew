using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FlashSkill : MonoBehaviour
{
    public SlimeBase parent;
    Vector3 vector3;
    public void HandleFlashSkill()
    {
        var temp = new List<GridBase>();
        foreach (var item in GamePlayController.Instance.playerContain.levelData.gridBasesId)
        {
            if (item.barrierBase == null)
            {
                temp.Add(item);
            }
        }
        temp.Shuffle();
    
        if (temp.Count > 0)
        {
            parent.fSMController.Stop();
            Debug.LogError(parent.gridBase.gameObject.name);
            parent.gridBase.barrierBase = null;
            parent.gridBase = null;
            vector3 = parent.transform.localScale;

            temp[0].spriteRenderer.color = new Color32(255, 174, 0, 255);

            if (parent.gameObject.GetComponent<MoveStateFlashBoss>().nextGrid != null)
            {
                parent.gameObject.GetComponent<MoveStateFlashBoss>().nextGrid.barrierBase = null;
            }
            if (parent.gameObject.GetComponent<MoveStateFlashBoss>().tempGrid != null)
            {
                parent.gameObject.GetComponent<MoveStateFlashBoss>().tempGrid.barrierBase = null;
            }
         
         
            parent.gameObject.GetComponent<MoveStateFlashBoss>().nextGrid = null;
            parent.gameObject.GetComponent<MoveStateFlashBoss>().tempGrid = null;
            parent.gameObject.GetComponent<MoveStateFlashBoss>().idOld.Clear();
            parent.gameObject.GetComponent<MoveStateFlashBoss>().idOld.Add(temp[0].id);
            parent.gridBase = temp[0];
            parent.spriteRenderer.DOColor(new Color32(0, 0, 0, 0), 0.1f).OnComplete(delegate
            {

                parent.transform.position = temp[0].transform.position;
                parent.spriteRenderer.DOColor(new Color32(255, 255, 255, 255), 0.1f).OnComplete(delegate
                {

                    temp[0].spriteRenderer.color = Color.white;
                    parent.fSMController.ChangeState(StateType.Move);
                });


            });

        }
    }
    }
