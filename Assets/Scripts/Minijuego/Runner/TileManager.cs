using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public float zSpawn = 0;
    public float longitudTile;
    public int numTiles;
    public List<GameObject> TilesActivos;

    public Transform cerdo;

    void Start()
    {
        for (int i = 0; i < numTiles; i++)
        {
            if (i == 0) InstanciarTile(0);
            else InstanciarTile(Random.Range(1, numTiles));
        }
    }

    void Update()
    {
        if(cerdo.position.z > zSpawn - (numTiles * longitudTile) + 80)
        {
            InstanciarTile(Random.Range(1, tiles.Length));
            BorrarTile();
        }
    }

    public void InstanciarTile(int index)
    {
        GameObject t = Instantiate(tiles[index], transform.forward * zSpawn, transform.rotation);
        TilesActivos.Add(t);
        zSpawn += longitudTile;
    }

    public void BorrarTile()
    {
        Destroy(TilesActivos[0]);
        TilesActivos.RemoveAt(0);
    }
}
