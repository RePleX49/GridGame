using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerScript : MonoBehaviour
{
    private const int WIDTH = 6;
    private const int HEIGHT = 10;

    private GameObject[,] TileGrid;
    public GameObject TilePrefab;
    public GameObject PlayerPrefab;
    private GameObject ActivePlayer;

    // Start is called before the first frame update
    void Start()
    {
        TileGrid = new GameObject[WIDTH, HEIGHT];
        MakeGrid();
        CenterCamera();

        TileScript ts = CheckMatches();
        while(ts != null)
        {
            ts.SetType();
            ts = CheckMatches();
        }

        Destroy(TileGrid[WIDTH / 2, HEIGHT / 2]);

        ActivePlayer = Instantiate(PlayerPrefab);
        ActivePlayer.transform.localPosition = new Vector2(WIDTH / 2, HEIGHT / 2);
        TileGrid[WIDTH / 2, HEIGHT / 2] = ActivePlayer;      
    }

    void MakeGrid()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                GameObject tile = Instantiate(TilePrefab);
                tile.transform.SetParent(this.transform);
                tile.transform.localPosition = new Vector2(x, y);

                TileGrid[x, y] = tile;
            }
        }
    }

    void CenterCamera()
    {
        Vector2 MiddleTilePos = TileGrid[WIDTH / 2, HEIGHT / 2].transform.position;
        Camera.main.gameObject.transform.position = new Vector3 (MiddleTilePos.x - 0.5f, MiddleTilePos.y - 0.5f, -10);
    }

    public TileScript CheckMatches()
    {
        for(int x = 0; x < WIDTH; x++) {
            for(int y = 0; y < HEIGHT; y++) {
                TileScript ts = TileGrid[x, y].GetComponent<TileScript>();

                if(x < WIDTH - 2 && ts.CompareTiles(TileGrid[x + 1, y], TileGrid[x + 2, y]))
                {
                    return ts;
                }
                else if ( y < HEIGHT -2 && ts.CompareTiles(TileGrid[x, y + 1], TileGrid[x, y + 2]))
                {
                    return ts;
                }
            }
        }

        return null;
    }

    void ClearMatch()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                TileScript ts = TileGrid[x, y].GetComponent<TileScript>();

                if (x < WIDTH - 2 && ts.CompareTiles(TileGrid[x + 1, y], TileGrid[x + 2, y]))
                {
                    ts.ClearMatch(TileGrid[x + 1, y], TileGrid[x + 2, y]);
                }
                else if (y < HEIGHT - 2 && ts.CompareTiles(TileGrid[x, y + 1], TileGrid[x, y + 2]))
                {
                    ts.ClearMatch(TileGrid[x, y + 1], TileGrid[x, y + 2]);
                }
            }
        }
    }

    public void MovePlayer(string Direction)
    {
        int PlayerX = (int)ActivePlayer.transform.position.x;
        int PlayerY = (int)ActivePlayer.transform.position.y;

        switch(Direction)
        {
            case "Right":
                TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX + 1, PlayerY];
                TileGrid[PlayerX, PlayerY].transform.position = new Vector2(PlayerX, PlayerY);

                TileGrid[PlayerX + 1, PlayerY] = ActivePlayer;
                ActivePlayer.transform.position = new Vector2(PlayerX + 1, PlayerY);

                break;

            case "Left":
                TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX - 1, PlayerY];
                TileGrid[PlayerX, PlayerY].transform.position = new Vector2(PlayerX, PlayerY);

                TileGrid[PlayerX - 1, PlayerY] = ActivePlayer;
                ActivePlayer.transform.position = new Vector2(PlayerX - 1, PlayerY);
                break;

            case "Up":
                TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX, PlayerY + 1];
                TileGrid[PlayerX, PlayerY].transform.position = new Vector2(PlayerX, PlayerY);

                TileGrid[PlayerX, PlayerY + 1] = ActivePlayer;
                ActivePlayer.transform.position = new Vector2(PlayerX, PlayerY + 1);
                break;

            case "Down":
                TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX, PlayerY - 1];
                TileGrid[PlayerX, PlayerY].transform.position = new Vector2(PlayerX, PlayerY);

                TileGrid[PlayerX, PlayerY - 1] = ActivePlayer;
                ActivePlayer.transform.position = new Vector2(PlayerX, PlayerY - 1);
                break;

            default:
                break;
        }

        
    }
}
