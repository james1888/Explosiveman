using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour {
    
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
            bool shouldContinue = ExplodeCell(originCell + new Vector3Int(i, 0, 0));
            if (!shouldContinue)
            {
                break;
            }
        }

        for (int i = 1; i <= power; i++)
        {

            bool shouldContinue = ExplodeCell(originCell + new Vector3Int(-i, 0, 0));
            if (!shouldContinue)
            {
                break;
            }
        }

        for (int i = 1; i <= power; i++)
        {
            bool shouldContinue = ExplodeCell(originCell + new Vector3Int(0, i, 0));
            if (!shouldContinue)
            {
                break;
            }
        }
        
        for (int i = 1; i <= power; i++)
        {

            bool shouldContinue = ExplodeCell(originCell + new Vector3Int(0, -i, 0));
            if (!shouldContinue)
            {
                break;
            }
        }


    }

    public bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if (tile == wallTile)
        {
            return false;
        }

        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
        }

        Vector3 pos = tilemap.GetCellCenterWorld(cell);

        Instantiate(explosionPrefab, pos, Quaternion.identity);

        return true;

    }

}
