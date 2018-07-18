using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{

    public Tilemap tilemap;

    public Tile wallTile;

    public Tile destructibleTile;

    public GameObject explosionPrefab;

    public int power = 2;

    public void Explode(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        ExplodeCell(originCell);
        for (int i = 1; i <= power; i++)
        {
            Vector3Int rightCell = originCell + new Vector3Int(i, 0, 0);
            if (getTile(rightCell) == wallTile) break;
            if (getTile(rightCell) == destructibleTile)
            {
                ExplodeCell(rightCell);
                break;
            }
            ExplodeCell(rightCell);
        }
        for (int i = 1; i <= power; i++)
        {
            Vector3Int leftCell = originCell + new Vector3Int(-i, 0, 0);
            if (getTile(leftCell) == wallTile) break;
            if (getTile(leftCell) == destructibleTile)
            {
                ExplodeCell(leftCell);
                break;
            }
            ExplodeCell(leftCell);
        }
        for (int i = 1; i <= power; i++)
        {
            Vector3Int topCell = originCell + new Vector3Int(0, i, 0);
            if (getTile(topCell) == wallTile) break;
            if (getTile(topCell) == destructibleTile)
            {
                ExplodeCell(topCell);
                break;
            }
            ExplodeCell(topCell);
        }
        for (int i = 1; i <= power; i++)
        {
            Vector3Int bottomCell = originCell + new Vector3Int(0, -i, 0);
            if (getTile(bottomCell) == wallTile) break;
            if (getTile(bottomCell) == destructibleTile)
            {
                ExplodeCell(bottomCell);
                break;
            }
            ExplodeCell(bottomCell);
        }
    }

    public void ExplodeCell(Vector3Int cell)
    {
        tilemap.SetTile(cell, null);
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);
    }

    public Tile getTile(Vector3Int cell)
    {
        return tilemap.GetTile<Tile>(cell);
    }

}
