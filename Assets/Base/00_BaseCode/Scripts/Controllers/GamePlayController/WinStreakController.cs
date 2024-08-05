using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using DG.Tweening;
using UnityEngine.Playables;
using Sirenix.OdinInspector;

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
    [Button]
 
    public void Init (Action callBack)
    {

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

                for (int i = 0; i < winStreak.lsStreakGifts.Count; i++)
                {
                    int index = i;
                    var temp = SimplePool2.Spawn(GameController.Instance.dataContain.giftDatabase.GetAnimItem(winStreak.lsStreakGifts[i].giftType));
                    temp.transform.position = lsProgesst[index + 1 ].postWinStreak.position;
                    temp.transform.parent = parentGift;
                    temp.transform.localScale = new Vector3(1, 1, 1);
                    temp.transform.SetAsFirstSibling();
                    temp.transform.DOMove(giftAnimNew.gameObject.transform.position, 0.5f ).OnComplete(delegate
                    {


                        if (index == UseProfile.WinStreak -1 || index == winStreak.lsStreakGifts.Count -1)
                        {
                            panelGift.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetDelay(0.5f).OnComplete(delegate
                            {
                                panelGift.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate
                                {
                                    callBack?.Invoke();
                                });
                            });
                        }
                        SimplePool2.Despawn(temp.gameObject);
                    });
                }


                //for (int i = 1; i < lsProgesst.Count; i++)
                //{
                //    int index = i;
                //    if (index <= UseProfile.WinStreak)
                //    {

                //        var temp = SimplePool2.Spawn(vfxTrail);
                //        temp.transform.position = lsProgesst[i].postWinStreak.position;
                //        temp.transform.parent = parentGift;
                //        temp.transform.localScale = new Vector3(13,13,13);
                //        temp.transform.SetAsFirstSibling();
                //        temp.transform.DOMove(giftAnimNew.gameObject.transform.position, 0.5f ).OnComplete(delegate {


                //            if (index == UseProfile.WinStreak)
                //            {
                //                //giftAnimNew.Play();
                //                //StartCoroutine(OnPlayableDirectorStopped());
                //                panelGift.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f).SetDelay(0.5f).OnComplete(delegate {

                //                    panelGift.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate {

                //                        callBack?.Invoke();

                //                    });


                //                });

                //            }
                //            SimplePool2.Despawn(temp.gameObject);
                //        });

                //    }
                //}
            }
        
         
            return;
        }
        else
        {

            canvasGroup.gameObject.SetActive(false);
            callBack?.Invoke();

        }



    }
    public void SetUp()
    {
    
    }    

    public void  HandleOpenBox(Action callBack)
    {
        panelWinStreak.gameObject.SetActive(true);
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 1;
        bgBlack.SetActive(false);
        panelWinStreak.SetActive(false);
        giftAnimNew.Play();
        StartCoroutine(OnPlayableDirectorStopped());
        IEnumerator OnPlayableDirectorStopped()
        {
            Debug.LogError("OnPlayableDirectorStopped");
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
                        canvasGroup.DOFade(0, 0.7f).OnComplete(delegate
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
