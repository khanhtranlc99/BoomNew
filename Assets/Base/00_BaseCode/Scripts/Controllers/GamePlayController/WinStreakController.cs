using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using DG.Tweening;

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
    public SkeletonGraphic giftAnim;
    public ItemInGame itemInGame;
    public CanvasGroup canvasGroup;
    public List<GiftInGame> lsGiftInGames;
    public GameObject objPackWinStreak;
    [Header("PackInShop")]
    public GameObject objPackShop;
    public SkeletonGraphic giftPackShop;
    public List<GiftInGame> lsGiftPackInGames;
    public List<WinStreakBar> lsProgesst;
    public GameObject panelGift;
    public GameObject panelWinStreak;
    public Transform parentGift;

    public void SetUp()
    { 
    
    
    }
    public void Init (Action callBack)
    {

        winStreak = new WinStreakGift();
        winStreak = GetWinStreakGift;

        if (winStreak != null)
        {
            panelWinStreak.gameObject.SetActive(true);
            panelGift.gameObject.SetActive(false);
            //canvasGroup.gameObject.SetActive(true);
            //canvasGroup.alpha = 1;
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
                panelGift.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(delegate {
                    panelWinStreak.gameObject.SetActive(false);
                    panelGift.gameObject.SetActive(true);
                    panelGift.GetComponent<CanvasGroup>().alpha = 0;
                    panelGift.GetComponent<CanvasGroup>().DOFade(1, 0.5f).OnComplete(delegate { ActionShow(); });
                  
                });
            }
            void ActionShow()
            {
               
                lsGiftInGames = new List<GiftInGame>();
                lsGiftPackInGames = new List<GiftInGame>();
                canvasGroup.gameObject.SetActive(true);
                canvasGroup.DOFade(1, 0.7f).OnComplete(delegate {
                    giftAnim.SetAnimation("anim", false, delegate {
                        giftAnim.SetAnimation("anim2", false, delegate {
                            giftAnim.SetAnimation("anim3", false, delegate {
                                for (int i = 0; i < winStreak.lsStreakGifts.Count; i++)
                                {
                                    lsGiftInGames.Add(new GiftInGame() { count = winStreak.lsStreakGifts[i].num, giftType = winStreak.lsStreakGifts[i].giftType });
                                }
                                for (int i = 0; i < lsGiftInGames.Count; i++)
                                {
                                    int index = i;
                                    var temp = SimplePool2.Spawn(itemInGame);
                                    temp.transform.parent = parentGift;
                                    temp.transform.position = parentGift.position;
                                    temp.transform.localScale = new Vector3(1, 1, 1);

                                    temp.Init(lsGiftInGames[index], new Vector3(-i, 3, 0), delegate {
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
                            });
                        });
                    });
                });
                if (UseProfile.Boom_Start == true || UseProfile.Fire_Start == true)
                {
                    objPackShop.SetActive(true);
                    giftPackShop.SetAnimation("anim", false, delegate {
                        giftPackShop.SetAnimation("anim2", false, delegate {
                            giftPackShop.SetAnimation("anim3", false, delegate {
                                if (UseProfile.Boom_Start == true)
                                {
                                    lsGiftPackInGames.Add(new GiftInGame() { count = 10, giftType = GiftType.Boom_Start });

                                }
                                if (UseProfile.Fire_Start == true)
                                {
                                    lsGiftPackInGames.Add(new GiftInGame() { count = 1, giftType = GiftType.Fire_Start });

                                }
                                for (int i = 0; i < lsGiftPackInGames.Count; i++)
                                {
                                    int index = i;
                                    var temp = SimplePool2.Spawn(itemInGame);
                                    temp.transform.parent = parentGift;
                                    temp.transform.position = parentGift.position;
                                    temp.transform.localScale = new Vector3(1, 1, 1);

                                    temp.Init(lsGiftPackInGames[index], new Vector3(2 - i, 3, 0), delegate {

                                    });
                                }
                            });
                        });
                    });
                }

            }
            return;
        }
        else
        {

            if (UseProfile.Boom_Start == true || UseProfile.Fire_Start == true)
            {
                panelWinStreak.gameObject.SetActive(false);
                panelGift.gameObject.SetActive(true);
                objPackShop.SetActive(true);
                objPackWinStreak.SetActive(false);
                lsGiftInGames = new List<GiftInGame>();
                lsGiftPackInGames = new List<GiftInGame>();
         
                    giftPackShop.SetAnimation("anim", false, delegate {
                        giftPackShop.SetAnimation("anim2", false, delegate {
                            giftPackShop.SetAnimation("anim3", false, delegate {
                                if (UseProfile.Boom_Start == true)
                                {
                                    lsGiftPackInGames.Add(new GiftInGame() { count = 10, giftType = GiftType.Boom_Start });

                                }
                                if (UseProfile.Fire_Start == true)
                                {
                                    lsGiftPackInGames.Add(new GiftInGame() { count = 1, giftType = GiftType.Fire_Start });

                                }
                                for (int i = 0; i < lsGiftPackInGames.Count; i++)
                                {
                                    int index = i;
                                    var temp = SimplePool2.Spawn(itemInGame);
                                    temp.transform.parent = parentGift;
                                    temp.transform.position = parentGift.position;
                                    temp.transform.localScale = new Vector3(1, 1, 1);

                                    temp.Init(lsGiftPackInGames[index], new Vector3(1 - i, 3, 0), delegate {
                                        if (index >= lsGiftPackInGames.Count - 1)
                                        {
                                            canvasGroup.DOFade(0, 0.7f).OnComplete(delegate
                                            {
                                                callBack?.Invoke();
                                                canvasGroup.gameObject.SetActive(false);
                                            });
                                        }
                                    });
                                }
                            });
                        });
                    });
          
            }
            else
            {
                canvasGroup.gameObject.SetActive(false);
                callBack?.Invoke();
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
