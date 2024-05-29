using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public GridBase gridBase;
    // Tilemap component
    public Tilemap tilemap;
    // Prefab của object muốn tạo
    public Transform parentTranform;
    // Danh sách các điểm đánh dấu (tọa độ trên tilemap)
    public List<Vector3Int> markedPoints;
    public Transform parentTransform;
    public LevelData levelData;

    [Button]
    void SpawnMap()
    {
        markedPoints = new List<Vector3Int>();

        // Lấy tất cả các ô có tile trong tilemap
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
 
        Debug.LogError("temp_" + bounds.xMax);
        Debug.LogError("temp_2_" + bounds.yMax);
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(tilePosition))
                {
                    markedPoints.Add(tilePosition);
                }
            }
        }

        Debug.Log("Collected marked points from tilemap.");
        if (markedPoints == null || markedPoints.Count == 0)
        {
            Debug.LogError("No marked points collected.");
            return;
        }
         levelData.gridArray = new GameObject[bounds.xMax, bounds.yMax];
        foreach (Vector3Int point in markedPoints)
        {
            // Chuyển đổi tọa độ tilemap sang tọa độ thế giới
            Vector3 worldPosition = tilemap.CellToWorld(point) + tilemap.tileAnchor;

            // Tạo object tại vị trí thế giới
            var temp = Instantiate(gridBase, worldPosition, Quaternion.identity);
            temp.transform.SetParent(parentTransform);
            if (point.x < 0 || point.x > levelData.gridArray.GetLength(0) || point.y < 0 || point.y > levelData.gridArray.GetLength(1))
            {
                Debug.LogWarning($"Position {point} is out of gridArray bounds.");
                continue; // Bỏ qua vị trí ngoài mảng
            }
            levelData.gridArray[point.x, point.y] = temp.gameObject;
            Debug.Log($"Spawned object at {worldPosition}");
        }
     
     

        Debug.Log("SpawnMap completed");
    }
}
