using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DieState : SlimeStateBase
{
    public GameObject vfxDie;
    SlimeTarget slimeTarget;
    public ItemInGame itemInGame;
    public int countCoin;
    public override void EndState()
    {

    }

    public override void Init(SlimeBase slimeBase)
    {
        data = slimeBase;
    }

    public override void StartState()
    {
 
        data.animator.Play("Die");
    }

    public override void UpdateState()
    {

    }

    public void HandleActionDie()
    {
        var tempGift = SimplePool2.Spawn(itemInGame);
        var currentGift = new GiftInGame() { giftType = GiftType.Coin, count = countCoin };
        tempGift.transform.parent = GamePlayController.Instance.gameScene.canvas.transform;
        tempGift.transform.position = this.transform.position;
        tempGift.transform.localScale = new Vector3(1, 1, 1);
        tempGift.Init(currentGift, this.transform);

        slimeTarget = GamePlayController.Instance.gameScene.targetController.GetSlimeTarget(data.slimeType);
        if (slimeTarget != null)
        {
            var temp = SimplePool2.Spawn(vfxDie);
            temp.transform.parent = GamePlayController.Instance.gameScene.canvas;
            temp.transform.position = this.transform.position;
            temp.transform.localScale = new Vector3(1, 1, 1);
            GamePlayController.Instance.gameScene.targetController.lsVfxDie.Add(temp);
            temp.transform.DOMove(slimeTarget.icon.position, 1).SetDelay(0.1f).SetEase(Ease.OutBack).OnComplete(delegate
            {
                GamePlayController.Instance.gameScene.targetController.lsVfxDie.Remove(temp);
                slimeTarget.HandleSubtraction();
                SimplePool2.Despawn(temp.gameObject);
            });
        }


        data.gridBase.barrierBase = null;
        GameController.Instance.questController.HandleCheckCompleteQuest(data.questTargetType);
        data.spriteRenderer.DOKill();
        Destroy(data.gameObject);
    }


}
