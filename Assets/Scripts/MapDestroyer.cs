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
        var originCell = tilemap.WorldToCell(worldPos);
        ExplodeCell(originCell);
        for (int i = 1; i <= power; i++)
        {
            var rightCell = originCell + new Vector3Int(i, 0, 0);
            var tile = getTile(rightCell);
            if (tile == null)
            {
                ExplodeCell(rightCell);
            }
            if (tile == destructibleTile)
            {
                ExplodeCell(rightCell);
                break;
            }
            if (tile == wallTile)
            {
                break;
            }
        }
        for (int i = 1; i <= power; i++)
        {
            var leftCell = originCell + new Vector3Int(-i, 0, 0);
            var tile = getTile(leftCell);
            if (tile == null)
            {
                ExplodeCell(leftCell);
            }
            if (tile == destructibleTile)
            {
                ExplodeCell(leftCell);
                break;
            }
            if (tile == wallTile)
            {
                break;
            }
        }
        for (int i = 1; i <= power; i++)
        {
            var topCell = originCell + new Vector3Int(0, i, 0);
            var tile = getTile(topCell);
            if (tile == null)
            {
                ExplodeCell(topCell);
            }
            if (tile == destructibleTile)
            {
                ExplodeCell(topCell);
                break;
            }
            if (tile == wallTile)
            {
                break;
            }
        }
        for (int i = 1; i <= power; i++)
        {
            var bottomCell = originCell + new Vector3Int(0, -i, 0);
            var tile = getTile(bottomCell);
            if (tile == null)
            {
                ExplodeCell(bottomCell);
            }
            if (tile == destructibleTile)
            {
                ExplodeCell(bottomCell);
                break;
            }
            if (tile == wallTile)
            {
                break;
            }
        }
    }

    public void ExplodeCell(Vector3Int cell)
    {
        tilemap.SetTile(cell, null);
        var pos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, pos, Quaternion.identity);
    }

    public Tile getTile(Vector3Int cell)
    {
        return tilemap.GetTile<Tile>(cell);
    }

}
