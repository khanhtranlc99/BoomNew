using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;
public class ItemInGame : MonoBehaviour
{

    public Image spriteRenderer;
    public Text tvCount;
    public GiftInGame currentGift;
    public PlayerContain playerContain;
    public CanvasGroup canvasGroup;
 
  
    public void Init(GiftInGame giftInGame, Transform post)
    {
        Debug.LogError("Boom_Start");

        currentGift = giftInGame;
        spriteRenderer.sprite = GameController.Instance.dataContain.giftDatabase.GetIconItem(currentGift.giftType) ;
        tvCount.text = giftInGame.count.ToString() ;
        playerContain = GamePlayController.Instance.playerContain;

        switch (currentGift.giftType)
        {
            case GiftType.Coin:
                GamePlayController.Instance.playerContain.totalCoin += currentGift.count;
                HandleJump(post.position, delegate {
                    HandleFadeOff();
                });
             

                break;
            case GiftType.Boom_Normal:
                HandleJump(post.position, delegate {
                    this.transform.DOMove(playerContain.boomInputController.iconBoom.transform.position , 0.5f).SetEase(Ease.InBack).SetDelay(0.5f).OnComplete(delegate {
                        GamePlayController.Instance.playerContain.boomInputController.HandlePlus(currentGift.count);
                        SimplePool2.Despawn(this.gameObject);
                    });
                });
                break;
            case GiftType.FlameUp_Item:
                HandleJump(post.position, delegate {
                    playerContain.flameUp_Item.ShowIcon(delegate {
                        this.transform.parent = playerContain.flameUp_Item.icon.transform;
                        this.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {
                            Debug.LogError("currentGift.count_" + currentGift.count);
                            UseProfile.FlameUp_Item += currentGift.count;
                            SimplePool2.Despawn(this.gameObject);
                        });
                    });            
                });
                break;
            case GiftType.FastBoom_Item:
          
                HandleJump(post.position, delegate {
                    playerContain.fastBoom_Item.ShowIcon(delegate { 
                        this.transform.parent = playerContain.fastBoom_Item.icon.transform;
                        this.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {
                            UseProfile.FastBoom_Item += currentGift.count;
                            SimplePool2.Despawn(this.gameObject);
                        });
                    });       
                });
                break;
            case GiftType.TimeBoom_Item:
            
                HandleJump(post.position, delegate {
                    playerContain.timeBoom_Item.ShowIcon(delegate { 
                        this.transform.parent = playerContain.timeBoom_Item.icon.transform; 
                      this.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InBack) .OnComplete(delegate {
                        UseProfile.TimeBoom_Item += currentGift.count;
                        SimplePool2.Despawn(this.gameObject);
                    });
                    
                    });
                 
                  
                });
                break;
            case GiftType.TNT_Booster:
                HandleJump(post.position, delegate {
                    this.transform.DOMove(playerContain.TNT_Booster.btnTNT_Booster.transform.position, 0.5f).SetEase(Ease.InBack).SetDelay(0.5f).OnComplete(delegate {
                        UseProfile.TNT_Booster += currentGift.count;
                        SimplePool2.Despawn(this.gameObject);
                    });
                });
                break;
            case GiftType.Rocket_Booster:
                HandleJump(post.position, delegate {
                    this.transform.DOMove(playerContain.rocket_Booster.rocket_Btn.transform.position, 0.5f).SetEase(Ease.InBack).SetDelay(0.5f).OnComplete(delegate {
                        UseProfile.Roket_Booster += currentGift.count;
                        SimplePool2.Despawn(this.gameObject);
                    });
                });
                break;
            case GiftType.Freeze_Booster:
                HandleJump(post.position, delegate {
                    this.transform.DOMove(playerContain.freeze_Booster.freezeBooster.transform.position, 0.5f).SetEase(Ease.InBack).SetDelay(0.5f).OnComplete(delegate {
                        UseProfile.Freeze_Booster += currentGift.count;
                        SimplePool2.Despawn(this.gameObject);
                    });
                });
                break;
            case GiftType.Atom_Booster:
                HandleJump(post.position, delegate {
                    this.transform.DOMove(playerContain.atomBoom_Booster.btnAtom_Booster.transform.position, 0.5f).SetEase(Ease.InBack).SetDelay(0.5f).OnComplete(delegate {
                        UseProfile.Atom_Booster += currentGift.count;
                        SimplePool2.Despawn(this.gameObject);
                    });
                });
                break;
            case GiftType.Boom_Start:

                HandleJump(post.position, delegate
                {


                    this.transform.DOMove(playerContain.boomInputController.iconBoom.transform.position, 1.5f).SetEase(Ease.InBack).OnComplete(delegate
                    {
                        playerContain.levelData.boomLimit += 5;
                
                        SimplePool2.Despawn(this.gameObject);
                    });

                });
                break;
            case GiftType.Fire_Start:

                HandleJump(post.position, delegate {
                    playerContain.flameUp_Item.ShowIcon(delegate {
                        this.transform.parent = playerContain.flameUp_Item.icon.transform;
                        this.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {

                            UseProfile.FlameUp_Item += currentGift.count;
                         
                            SimplePool2.Despawn(this.gameObject);
                        });
                    });
                });
                break;
           
        }
    }

    public void Init(GiftInGame giftInGame, Vector3 post, Action callBack)
    {
      

        currentGift = giftInGame;
        spriteRenderer.sprite = GameController.Instance.dataContain.giftDatabase.GetIconItem(currentGift.giftType);
        tvCount.text = giftInGame.count.ToString();
        playerContain = GamePlayController.Instance.playerContain;

        switch (currentGift.giftType)
        {
           
            case GiftType.FlameUp_Item:
                HandleJump(post, delegate {
                    playerContain.flameUp_Item.ShowIcon(delegate {
                        this.transform.parent = playerContain.flameUp_Item.icon.transform;
                        this.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {
                       
                            UseProfile.FlameUp_Item += currentGift.count;
                            callBack?.Invoke();
                            SimplePool2.Despawn(this.gameObject);
                        });
                    });
                });
                break;
            case GiftType.FastBoom_Item:

                HandleJump(post, delegate {
                    playerContain.fastBoom_Item.ShowIcon(delegate {
                        this.transform.parent = playerContain.fastBoom_Item.icon.transform;
                        this.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {
                            UseProfile.FastBoom_Item += currentGift.count;
                            callBack?.Invoke();
                            SimplePool2.Despawn(this.gameObject);
                        });
                    });
                });
                break;
            case GiftType.TimeBoom_Item:

                HandleJump(post, delegate {
                    playerContain.timeBoom_Item.ShowIcon(delegate {
                        this.transform.parent = playerContain.timeBoom_Item.icon.transform;
                        this.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {
                            UseProfile.TimeBoom_Item += currentGift.count;
                            callBack?.Invoke();
                            SimplePool2.Despawn(this.gameObject);
                        });
                    });
                });
                break;
            case GiftType.Boom_Normal:

               
                HandleJump(post, delegate
                {


                    this.transform.DOMove(playerContain.boomInputController.iconBoom.transform.position, 1.5f).SetEase(Ease.InBack).OnComplete(delegate
                    {
                        playerContain.levelData.boomLimit += 5;
                        callBack?.Invoke();
                        SimplePool2.Despawn(this.gameObject);
                    });

                });
                break;
            case GiftType.Boom_Start:

                
                HandleJump(post, delegate
                {

 
                    this.transform.DOMove(playerContain.boomInputController.iconBoom.transform.position, 1.5f).SetEase(Ease.InBack).OnComplete(delegate
                    {
                        playerContain.levelData.boomLimit += 10;
                        callBack?.Invoke();
                        SimplePool2.Despawn(this.gameObject);
                    });

                });
                break;
            case GiftType.Fire_Start:

                HandleJump(post, delegate {
                    playerContain.flameUp_Item.ShowIcon(delegate {
                        this.transform.parent = playerContain.flameUp_Item.icon.transform;
                        this.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(delegate {

                            UseProfile.FlameUp_Item += currentGift.count;
                            callBack?.Invoke();
                            SimplePool2.Despawn(this.gameObject);
                        });
                    });
                });
                break;

        }
    }
    public void HandleJump(Vector3 paramPost, Action callBack)
    {
        this.transform.DOJump(paramPost, 1,1,1).SetEase(Ease.Linear).OnComplete(delegate {
         
            if(callBack != null)
            {
                callBack?.Invoke();
            }
        
        });
    }

  

    public void HandleFadeOff()
    {
        canvasGroup.DOFade(0,0.5f).SetDelay(0.7f).OnComplete(delegate { SimplePool2.Despawn(this.gameObject); });
     
    }

    private void OnDisable()
    {
        canvasGroup.alpha = 1;
    }
}
