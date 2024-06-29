using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
public class TimeBoom_Item : MonoBehaviour
{

    public int num;
    public Text tvNum;
    public GameObject icon;
    public Button btnTimeBoom;
    public bool wasUseTimeBoom;
    public TimeBoom timeBoom;
    public void Init()
    {
        if (UseProfile.TimeBoom_Item >= 1)
        {
            icon.SetActive(true);
            tvNum.text = "" + UseProfile.TimeBoom_Item;
            GamePlayController.Instance.playerContain.tutorial_TimeBoom.StartTut();
        }
        btnTimeBoom.onClick.AddListener(delegate { HandleTimeBoom(); });
        EventDispatcher.EventDispatcher.Instance.RegisterListener(EventID.TIMEBOOM_ITEM, HandleShowTimeBoom_Itemp);
    }

    public void HandleShowTimeBoom_Itemp(object param)
    {
        if (UseProfile.TimeBoom_Item >= 1)
        {

            icon.SetActive(true);
            icon.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f).OnComplete(delegate {
                icon.transform.DOScale(new Vector3(0.35f, 0.35f, 0.35f), 0.5f);
            });
            tvNum.text = "" + UseProfile.FastBoom_Item;
        }
        else
        {
            icon.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f).OnComplete(delegate {
                icon.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(delegate {
                    icon.gameObject.SetActive(false);
                });
            });
        }
    }
    public void ShowIcon(Action callBack)
    {
        if(!icon.gameObject.activeSelf)
        {
            icon.transform.localScale = Vector3.zero;
            icon.SetActive(true);
            icon.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f).OnComplete(delegate {
               
                icon.transform.DOScale(new Vector3(0.35f, 0.35f, 0.35f), 0.5f).OnComplete(delegate {
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
    private void HandleTimeBoom()
    {
        GamePlayController.Instance.playerContain.boomInputController.enabled = false;
        GamePlayController.Instance.playerContain.tutorial_TimeBoom.NextTut();
        wasUseTimeBoom = true;
        btnTimeBoom.interactable = false;
    }


    private void Update()
    {
        if (wasUseTimeBoom)
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

                                var boom = SimplePool2.Spawn(timeBoom, selectedObject.transform.position, Quaternion.identity);
                                selectedObject.barrierBase = boom;
                                boom.gridBase = selectedObject;
                                GamePlayController.Instance.playerContain.boomInputController.enabled = true;
                                btnTimeBoom.interactable = true;
                                wasUseTimeBoom = false;
                                UseProfile.TimeBoom_Item -= 1;
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
        EventDispatcher.EventDispatcher.Instance.RemoveListener(EventID.TIMEBOOM_ITEM, HandleShowTimeBoom_Itemp);
    }
}
