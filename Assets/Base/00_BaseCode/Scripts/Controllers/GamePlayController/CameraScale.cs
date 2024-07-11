﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
    private Camera cam;
    public float Speed;
 
    public void Init()
    {
        cam = Camera.main;
        StartCoroutine(FixScreen(GamePlayController.Instance.playerContain.levelData.postLeft.transform.position, GamePlayController.Instance.playerContain.levelData.postRight.transform.position));
    }

    public IEnumerator FixScreen(Vector3 left, Vector3 right)
    {
        float speed = Speed;

        // Tiếp tục scale cho đến khi cả hai điểm đều nằm trong khung nhìn cả trục x và trục y
        while (!IsPointVisible(left) || !IsPointVisible(right))
        {
            if (cam.orthographic)
            {
                cam.orthographicSize += speed;
            }
            else
            {
                cam.fieldOfView += speed;
            }
            speed += 0.001f; // Điều chỉnh tăng tốc độ nếu cần thiết
            yield return null;
        }
    }

    // Hàm kiểm tra xem một điểm có nằm trong khung nhìn hay không
    private bool IsPointVisible(Vector3 point)
    {
        Vector3 viewportPoint = cam.WorldToViewportPoint(point);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }
}