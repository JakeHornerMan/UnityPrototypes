using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform cam;
    private Dictionary<Vector2, Tile> tiles;

    void Start()
    {
        generateGrid();
    }

    void Update()
    {
        
    }

    void generateGrid() {
        tiles = new Dictionary<Vector2, Tile>();
        for(int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                var spawnTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnTile.name = $"Tile {x} {y}";

                var isOffset = (x%2 == 0 && y%2 != 0) || (x%2 != 0 && y%2 == 0);
                spawnTile.SetColor(isOffset);

                tiles[new Vector2(x,y)] = spawnTile;
            }
        }

        cam.transform.position = new Vector3((float)width/2 -0.5f, (float)height/2 -0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos){
        if(tiles.TryGetValue(pos, out var tile)){
            Debug.Log(tile.transform.position);
            return tile;
        }
        return null;
    }    
}
