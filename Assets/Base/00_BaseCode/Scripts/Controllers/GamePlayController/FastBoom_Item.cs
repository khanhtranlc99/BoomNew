using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FastBoom_Item : MonoBehaviour
{
    public int num;
    public Text tvNum;
    public GameObject icon;
    public Button btnFastBoom;
    public bool wasUseFastBoom;
    public FastBoom fastBoom;
    public void Init()
    {
        if (UseProfile.FastBoom_Item >= 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.FastBoom_Item;
            GamePlayController.Instance.playerContain.tutorial_FastBoom.StartTut();
        }
        wasUseFastBoom = false;
        btnFastBoom.onClick.AddListener(delegate { HandleBtnFastBoom(); });
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.FASTBOOM_ITEM, HandleShowFastBoom_Item);
    }

    public void HandleShowFastBoom_Item(object param)
    {
        if (UseProfile.FastBoom_Item >= 1)
        {
          
            icon.SetActive(true);
            icon.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(delegate {
                icon.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            });
            tvNum.text = "" + UseProfile.FastBoom_Item;
        }
        else
        {
            icon.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(delegate {
                icon.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(delegate {
                    icon.gameObject.SetActive(false);
                });
            });
        }
        Debug.LogError("HandleShowFastBoom_Item_" + UseProfile.FastBoom_Item);
    }

    public void ShowIcon(Action callBack)
    {
        if (!icon.gameObject.activeSelf)
        {
            icon.transform.localScale = Vector3.zero;
            icon.SetActive(true);
            icon.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(delegate {

                icon.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(delegate {
                    if (callBack != null)
                    {
                        callBack?.Invoke();
                    }

                });
            });
        }
        else
        {
            if (callBack != null)
            {
                callBack?.Invoke();
            }
        }


    }

    public void HandleBtnFastBoom()
    {
        GamePlayController.Instance.playerContain.boomInputController.enabled = false;
        GamePlayController.Instance.playerContain.tutorial_FastBoom.NextTut();
        wasUseFastBoom = true;
        btnFastBoom.interactable = false;
    }
    private void Update()
    {
        if(wasUseFastBoom)
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
                    GridBase selectedObject = hit.collider.gameObject.GetComponent<GridBase>();
                    if (selectedObject != null)
                    {
                        if (selectedObject.gameObject.tag == "Grid")
                        {
                            // Kiểm tra xem đối tượng đã chọn có bị che bởi đối tượng E không

                            if (selectedObject.barrierBase == null && selectedObject.isFree == false)
                            {

                                var boom = SimplePool2.Spawn(fastBoom, selectedObject.transform.position, Quaternion.identity);
                                selectedObject.barrierBase = boom;
                                boom.gridBase = selectedObject;
                                GamePlayController.Instance.playerContain.boomInputController.enabled = true;
                                btnFastBoom.interactable = true;
                                wasUseFastBoom = false;
                                UseProfile.FastBoom_Item -= 1;
                            }
                        }
                    }
                    // Kiểm tra xem đối tượng đã chọn có phải là A, B, C hoặc D không

                }
            }
        }
    }
    private void OnDestroy()
    {
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.FASTBOOM_ITEM, HandleShowFastBoom_Item);
    }
}
