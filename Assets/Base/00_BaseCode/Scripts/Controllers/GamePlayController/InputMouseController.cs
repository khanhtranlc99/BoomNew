using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouseController : MonoBehaviour
{
    public Boom prefabBoomBase;


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

                        if (selectedObject.barrierBase == null)
                        {
                            var boom = SimplePool2.Spawn(prefabBoomBase, selectedObject.transform.position, Quaternion.identity);
                            selectedObject.barrierBase = boom;
                            boom.gridBase = selectedObject;
                        }


                    }
                }
                // Kiểm tra xem đối tượng đã chọn có phải là A, B, C hoặc D không
             
            }
        }
    }
}
