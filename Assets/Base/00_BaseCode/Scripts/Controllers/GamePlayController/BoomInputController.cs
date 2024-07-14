using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using MoreMountains.NiceVibrations;

public class BoomInputController : MonoBehaviour
{
    public Boom prefabBoomBase;
    public Text tvBoom;
    public Image iconBoom;
    LevelData levelData;
    public int countBoom = 0;
    bool tweenText;
    public AudioClip click;

    public void Init (LevelData level)
    {
        levelData = level;
        tvBoom.text = "" + levelData.boomLimit;
        countBoom = levelData.boomLimit;
        tweenText = false;
    }

    private void HandleSubtraction()
    {
        countBoom -= 1;
        tvBoom.text = "" + countBoom;

        iconBoom.transform.DOKill();
        iconBoom.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).OnComplete(delegate {
            iconBoom.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        });

        if (countBoom <= 3 && !tweenText)
        {
            tweenText = true;
            TweenText();
        }

    }
    public void HandlePlus(int param)
    {
        countBoom += param;
        tvBoom.text = "" + countBoom;
        iconBoom.transform.DOKill();
        iconBoom.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).OnComplete(delegate {
            iconBoom.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        });

      
       
    }

    private void TweenText()
    {
        tvBoom.DOColor(Color.red,  0.5f).OnComplete(delegate {

            tvBoom.DOColor(Color.white, 0.5f).OnComplete(delegate {

                TweenText();
            });

        });
    }
    

    // Update is called once per frame
    void Update()
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
                            if(countBoom > 0)
                            {
                                var boom = SimplePool2.Spawn(prefabBoomBase, selectedObject.transform.position, Quaternion.identity);
                                selectedObject.barrierBase = boom;
                                boom.gridBase = selectedObject;
                                boom.Init();
                                HandleSubtraction();
                                GamePlayController.Instance.playerContain.tutorial_BoomInput.NextTut();
                                GamePlayController.Instance.playerContain.tutorial_TNT.StartTut();
                                GamePlayController.Instance.playerContain.tutorial_Rocket.StartTut();
                                GamePlayController.Instance.playerContain.tutorial_Freeze.StartTut();
                                GamePlayController.Instance.playerContain.tutorial_Atom.StartTut();
                                GameController.Instance.musicManager.PlayOneShot(click);
                                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                            }
                            
                        }
                    }
                    //if (selectedObject.barrierBase != null)
                    //{
                    //    if (selectedObject.barrierBase.GetComponent<TimeBoom>() != null )
                    //    {
                    //        selectedObject.barrierBase.GetComponent<TimeBoom>().HandleExplosion();
                    //    }
                    //}
                }
                // Kiểm tra xem đối tượng đã chọn có phải là A, B, C hoặc D không
             
            }
        }
    }

    private void OnDestroy()
    {
        tvBoom.DOKill();
        iconBoom.DOKill();
    }
}
