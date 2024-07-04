using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class QuestBar : MonoBehaviour
{
    public Image barAmount;
    public Image iconTarget;
    public Image iconGift;
    public Text tvContent;
    public Text tvContent_2;
    public Quest currentQuest;
    public List<TargetData> lsTargetDatas;
    public Text tvCount;
    public TargetData GetTargetData(QuestTargetType param)
    {
        foreach(var item in lsTargetDatas)
        {
            if(item.barrierType == param)
            {
                return item;
            }    
        }
        return null;
    }
    public void Init ()
    {
        currentQuest = GameController.Instance.questController.currentQuest;
        if(currentQuest != null)
        {
            iconTarget.sprite = GetTargetData(currentQuest.targetType).icon;
            iconGift.sprite = GameController.Instance.dataContain.giftDatabase.GetIconItem(currentQuest.giftType);
            tvContent.text = QuestController.ProgessQuest + "/" + currentQuest.numTarget;
            tvContent_2.text = QuestController.ProgessQuest + "/" + currentQuest.numTarget;
            if (QuestController.ProgessQuest < currentQuest.numTarget)
            {
                float temp = (float)QuestController.ProgessQuest / currentQuest.numTarget;
     
                barAmount.DOFillAmount(temp, 0.7f);
            }
            else
            {
                barAmount.DOFillAmount(1, 0.7f).OnComplete(delegate { HandleClaimGift(); });
            }
            tvCount.text = "" + currentQuest.numGift;
        }
    }


    private void HandleClaimGift()
    {
        List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();
        giftRewardShows.Add(new GiftRewardShow() { amount = currentQuest.numGift, type = currentQuest.giftType });
        GameController.Instance.questController.ResetQuest();
        barAmount.fillAmount = 0;
        Init();
        PopupRewardBase.Setup(false).Show(giftRewardShows, delegate { });
    }    
}
[System.Serializable]
public class TargetData
{
    public QuestTargetType barrierType;
    public Sprite icon;
}