using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using DG.Tweening;
using UnityEngine.Playables;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class WinStreakController : MonoBehaviour
{
    public List<WinStreakGift> lsWinStreakGifts;
    public WinStreakGift GetWinStreakGift
    {
        get
        {
            if(UseProfile.WinStreak > 0)
            {
                return lsWinStreakGifts[UseProfile.WinStreak - 1];
            }    
            else
            {
                return null;
            }                 
        }
    }
    public WinStreakGift winStreak;
 
    public ItemInGame itemInGame;
    public CanvasGroup canvasGroup;
    public List<GiftInGame> lsGiftInGames;
 
 
    public List<WinStreakBar> lsProgesst;


    public GameObject panelGift;
    public GameObject panelWinStreak;
    public Transform parentGift;

    public ParticleSystem vfxTrail;
    public PlayableDirector giftAnimNew;
    public GameObject vfxGift;

    public GameObject bgBlack;
    public GameObject vfxGiftTemp;

    public GameObject prefabParent;
    public Text tvWinStreak;

    public void Init (Action callBack)
    {
        tvWinStreak.text = "Win Streak";
        callBackGift = callBack;
        countWinStreak = 0;
        winStreak = new WinStreakGift();
        winStreak = GetWinStreakGift;

        if (winStreak != null)
        {
            bgBlack.SetActive(true);
            panelWinStreak.SetActive(true);
            panelWinStreak.gameObject.SetActive(true);
            if(!vfxGift.activeSelf)
            {
                vfxGift = vfxGiftTemp;
                panelGift = vfxGiftTemp;
            }    
            vfxGift.SetActive(true);
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1;

            for (int i = 0; i < lsProgesst.Count; i++)
            {
                int index = i;
                if (index <= UseProfile.WinStreak)
                {
                    lsProgesst[index].gameObject.SetActive(true);
                    if (index == UseProfile.WinStreak)
                    {
                        lsProgesst[index].Scale(delegate { ActiveCanvasGift(); });
                    }
                }
            }
            void ActiveCanvasGift()
            {

                for (int i = 1; i < lsProgesst.Count; i++)
                {
                    int index = i;
                    if(lsProgesst[index].gameObject.activeSelf)
                    {
                        lsProgesst[index  ].postWinStreak.gameObject.GetComponent<Animator>().Play("WinStreak_" + index);
                    }
                 
                    //var temp = SimplePool2.Spawn(GameController.Instance.dataContain.giftDatabase.GetAnimItem(winStreak.lsStreakGifts[i].giftType));
                    //temp.transform.position = lsProgesst[index + 1 ].postWinStreak.position;
                    //temp.transform.parent = panelGift.gameObject.transform;
                    //temp.transform.localScale = new Vector3(1, 1, 1);
                    //temp.transform.SetAsFirstSibling();
                    //MoveInCurve(temp, delegate {

                    //    if (index == UseProfile.WinStreak - 1 || index == winStreak.lsStreakGifts.Count - 1)
                    //    {
                    //        panelGift.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetDelay(0.5f).OnComplete(delegate
                    //        {
                    //            panelGift.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate
                    //            {
                    //                callBack?.Invoke();
                    //            });
                    //        });
                    //    }
                    //    SimplePool2.Despawn(temp.gameObject);

                    //});
                    //temp.transform.DOMove(giftAnimNew.gameObject.transform.position, 0.5f ).OnComplete(delegate
                    //{

                    //});
                }
            }             
            return;
        }
        else
        {

            canvasGroup.gameObject.SetActive(false);
            callBack?.Invoke();

        }
    }

    int countWinStreak = 0;
    Action callBackGift;
    public void HandleWinStreak()
    {
        countWinStreak += 1;
        if(countWinStreak >= UseProfile.WinStreak)
        {
            panelGift.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).OnComplete(delegate
            {
                panelGift.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate
                {
                    callBackGift?.Invoke();
                });
            });
        }
    }

    public void MoveInCurve(GameObject param, Action callBack)
    {

        Vector3 center = new Vector3((param.transform.position.x + param.transform.parent.position.x) / 2, (param.transform.position.y + param.transform.parent.position.y) / 2);

        Vector3 center_2 = new Vector3((center.x + param.transform.parent.position.x) / 2 - 3, (center.y) + 2);
        Vector3 centerUp = new Vector3((center.x + param.transform.parent.position.x) / 2 - 2, (center.y + param.transform.parent.position.y) / 2 + 2);
        Vector3 centerDown = new Vector3((param.transform.position.x + center.x) / 2 - 2, (param.transform.position.y + center.y) / 2 + 1);



        //var tempCenterUp = Instantiate(prefabParent, centerUp, Quaternion.identity);
        //tempCenterUp.transform.parent = panelGift.gameObject.transform;
        //tempCenterUp.transform.localScale = new Vector3(1, 1, 1);

        //var tempCenter = Instantiate(prefabParent, center_2, Quaternion.identity);
        //tempCenter.transform.parent = panelGift.gameObject.transform;
        //tempCenter.transform.localScale = new Vector3(1, 1, 1);

        //var tempCenterDown = Instantiate(prefabParent, centerDown, Quaternion.identity);
        //tempCenterDown.transform.parent = panelGift.gameObject.transform;
        //tempCenterDown.transform.localScale = new Vector3(1, 1, 1);

        param.transform.DOMove(centerDown, 0.2f).SetEase(Ease.Linear).OnComplete(delegate
        {
            param.transform.DOMove(center_2, 0.2f).SetEase(Ease.Linear).OnComplete(delegate
            {
                param.transform.DOMove(centerUp, 0.2f).SetEase(Ease.Linear).OnComplete(delegate
                {
                    param.transform.DOMove(panelGift.gameObject.transform.position, 0.2f).SetEase(Ease.Linear).OnComplete(delegate { callBack?.Invoke(); });

                });
            });
        });

    }

    public void  HandleOpenBox(Action callBack)
    {
        tvWinStreak.text = "Win Streak Prize";
        panelWinStreak.gameObject.SetActive(true);
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        bgBlack.SetActive(false);
        panelWinStreak.SetActive(false);
        giftAnimNew.Play();
        StartCoroutine(OnPlayableDirectorStopped());
        IEnumerator OnPlayableDirectorStopped()
        {
           
            winStreak = new WinStreakGift();
            winStreak = GetWinStreakGift;
            yield return new WaitForSeconds(2);
            vfxGift.SetActive(false);
            lsGiftInGames = new List<GiftInGame>();

            for (int i = 0; i < winStreak.lsStreakGifts.Count; i++)
            {
                lsGiftInGames.Add(new GiftInGame() { count = winStreak.lsStreakGifts[i].num, giftType = winStreak.lsStreakGifts[i].giftType });
            }
            for (int i = 0; i < lsGiftInGames.Count; i++)
            {
                int index = i;
                var temp = SimplePool2.Spawn(itemInGame);
                temp.transform.parent = parentGift;
                temp.transform.position = vfxGift.transform.position;
                temp.transform.localScale = new Vector3(1, 1, 1);

                temp.Init(lsGiftInGames[index], new Vector3(i, 3, 0), delegate
                {
                    if (index >= lsGiftInGames.Count - 1)
                    {
                        canvasGroup.DOFade(0, 0.5f).OnComplete(delegate
                        {
                            callBack?.Invoke();
                            canvasGroup.gameObject.SetActive(false);
                        });
                    }
                });
            }
        }
    
    }



    private void ShowGift(Action callBack)
    {
      
    }    

    private IEnumerator SpawnGift()
    {
        yield return new WaitForSeconds(1);
    }    



}
[System.Serializable]
public class WinStreakGift
{
    public List<StreakGift> lsStreakGifts;

}

[System.Serializable]
public class StreakGift
{
    public int num;
    public GiftType giftType;

}
