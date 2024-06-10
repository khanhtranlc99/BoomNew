using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class AtomBoom_Booster : MonoBehaviour
{
    public Button btnAtom_Booster;
    public Text tvNum;
    public GameObject objNum;
    public GameObject parentTvCoin;
    public GameObject lockIcon;
    public GameObject unLockIcon;

    public Atom_Boom atom_Prefab;
    public bool wasUseTNT_Booster;

    public Transform post;
    GameObject selectedObject;
    public FlameVfx flameVfx;
    private FlameVfx currentFlameVfx;
    public GameObject panelTut;
    public CanvasGroup canvasGroup;
    public void Init()
    {
        selectedObject = null;
        wasUseTNT_Booster = false;
        if (UseProfile.CurrentLevel >= 1)
        {

            unLockIcon.gameObject.SetActive(true);
            lockIcon.gameObject.SetActive(false);
            HandleUnlock();
            Debug.LogError("HandleUnlock");

        }
        else
        {
            unLockIcon.gameObject.SetActive(false);
            lockIcon.gameObject.SetActive(true);
            objNum.SetActive(false);
            HandleLock();
            Debug.LogError("HandleLock");
        }


        void HandleUnlock()
        {
            btnAtom_Booster.onClick.AddListener(HandleAtom_Booster);
            if (UseProfile.Atom_Booster > 0)
            {
                objNum.SetActive(true);
                tvNum.text = UseProfile.Atom_Booster.ToString();
                parentTvCoin.SetActive(false);
            }
            else
            {
                objNum.SetActive(false);
                tvNum.gameObject.SetActive(false);
                parentTvCoin.SetActive(true);
            }
            EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.CHANGE_ATOM_BOOSTER, ChangeText);
        }
        void HandleLock()
        {


            btnAtom_Booster.onClick.AddListener(HandleLockBtn);
        }
    }

    public void HandleLockBtn()
    {
        GameController.Instance.musicManager.PlayClickSound();
        GameController.Instance.moneyEffectController.SpawnEffectText_FlyUp
                              (
                              btnAtom_Booster.transform.position,
                              "Unlock at level 2",
                              Color.white,
                              isSpawnItemPlayer: true
                              );
    }





    public void HandleAtom_Booster()
    {
        if(UseProfile.Atom_Booster >= 1)
        {
            GamePlayController.Instance.gameScene.HideBotUI(delegate {
                panelTut.SetActive(true);
                canvasGroup.DOFade(1, 0.3f);
            });
            UseProfile.Atom_Booster -= 1;
            GamePlayController.Instance.playerContain.boomInputController.enabled = false;
            wasUseTNT_Booster = true;
        }
        else
        {
            SuggetBox.Setup(GiftType.Atom_Booster).Show();
        }
 
    }


    void Update()
    {
        if (wasUseTNT_Booster)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Lấy vị trí của con trỏ chuột trong không gian 2D
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Tạo một tia chớp từ vị trí của con trỏ chuột
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                // Kiểm tra xem tia chớp có chạm vào bất kỳ đối tượng nào không
                if (hit.collider != null)
                {

                    if (currentFlameVfx == null)
                    {
                        currentFlameVfx = SimplePool2.Spawn(flameVfx);
                        currentFlameVfx.Init();
                    }

                }
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition1, Vector2.zero);
                if (currentFlameVfx != null)
                {
                    currentFlameVfx.transform.position = mousePosition1;
                }

                if (hit.collider != null)
                {
                    selectedObject = hit.collider.gameObject;
                }


            }
            if (Input.GetMouseButtonUp(0))
            {
                if (selectedObject != null)
                {
                    var boom = SimplePool2.Spawn(atom_Prefab, post.position /*selectedObject.transform.position*/, Quaternion.identity);
                    boom.transform.DOMove(selectedObject.transform.position, 0.35f).SetEase(Ease.OutFlash).OnComplete(delegate { boom.HandleExplosion(); });
                    wasUseTNT_Booster = false;
                    GamePlayController.Instance.playerContain.boomInputController.enabled = true;
                    selectedObject = null;
                    canvasGroup.DOFade(0, 0.3f).OnComplete(delegate {
                        GamePlayController.Instance.gameScene.ShowBotUI(delegate {

                            panelTut.SetActive(false);

                        });
                    });
                    if (currentFlameVfx != null)
                    {
                        SimplePool2.Despawn(currentFlameVfx.gameObject);
                        currentFlameVfx = null;
                    }

                }
            }
        }

    }

    public void ChangeText(object param)
    {
        tvNum.text = UseProfile.Atom_Booster.ToString();
        if (UseProfile.Atom_Booster > 0)
        {
            tvNum.gameObject.SetActive(true);
            tvNum.text = UseProfile.Atom_Booster.ToString();
            parentTvCoin.SetActive(false);
        }
        else
        {
            objNum.SetActive(false);
            tvNum.gameObject.SetActive(false);
            parentTvCoin.SetActive(true);
        }
    }
    public void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.CHANGE_TNT_BOOSTER, ChangeText);
    }
}
