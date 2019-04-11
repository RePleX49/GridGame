using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerScript : MonoBehaviour
{
    private const int WIDTH = 6;
    private const int HEIGHT = 10;

    public GameObject[,] TileGrid;
    public GameObject TilePrefab;
    public GameObject PlayerPrefab;
    private GameObject ActivePlayer;

    public static GridManagerScript Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

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

    public void MovePlayer(string Direction)
    {
        int PlayerX = (int)ActivePlayer.transform.position.x;
        int PlayerY = (int)ActivePlayer.transform.position.y;

        switch(Direction)
        {
            case "Right":
                if(PlayerX < WIDTH)
                {
                    TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX + 1, PlayerY];
                    TileGrid[PlayerX, PlayerY].transform.position = new Vector2(PlayerX, PlayerY);

                    TileGrid[PlayerX + 1, PlayerY] = ActivePlayer;
                    ActivePlayer.transform.position = new Vector2(PlayerX + 1, PlayerY);
                }

                break;

            case "Left":
                if(PlayerX > 0)
                {
                    TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX - 1, PlayerY];
                    TileGrid[PlayerX, PlayerY].transform.position = new Vector2(PlayerX, PlayerY);

                    TileGrid[PlayerX - 1, PlayerY] = ActivePlayer;
                    ActivePlayer.transform.position = new Vector2(PlayerX - 1, PlayerY);
                }

                break;

            case "Up":
                if(PlayerY < HEIGHT)
                {
                    TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX, PlayerY + 1];
                    TileGrid[PlayerX, PlayerY].transform.position = new Vector2(PlayerX, PlayerY);

                    TileGrid[PlayerX, PlayerY + 1] = ActivePlayer;
                    ActivePlayer.transform.position = new Vector2(PlayerX, PlayerY + 1);
                }

                break;

            case "Down":
                if(PlayerY > 0)
                {
                    TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX, PlayerY - 1];
                    TileGrid[PlayerX, PlayerY].transform.position = new Vector2(PlayerX, PlayerY);

                    TileGrid[PlayerX, PlayerY - 1] = ActivePlayer;
                    ActivePlayer.transform.position = new Vector2(PlayerX, PlayerY - 1);
                }

                break;

            default:
                break;
        }

        ClearMatches();
    }

    void ClearMatches()
    {
        GameObject InitialTile = null;
        GameObject SecondaryTile = null;
        List<GameObject> MatchingTiles = new List<GameObject>();
        for(int x = 0; x < WIDTH; x++)
        {
            for(int y = 0; y < HEIGHT; y++)
            {
                if(TileGrid[x,y].CompareTag("Tile"))
                {
                    if (InitialTile == null)
                    {
                        InitialTile = TileGrid[x, y];
                    }

                    if (InitialTile.GetComponent<TileScript>().TileType == TileGrid[x, y].GetComponent<TileScript>().TileType && !SecondaryTile)
                    {
                        MatchingTiles.Add(TileGrid[x, y]);

                        if(TileGrid[x, y] != InitialTile)
                        {
                            SecondaryTile = TileGrid[x, y];
                        }
                        
                    }
                    else if(SecondaryTile.GetComponent<TileScript>().TileType == TileGrid[x, y].GetComponent<TileScript>().TileType)
                    {
                        MatchingTiles.Add(TileGrid[x, y]);
                    }
                    else
                    {
                        MatchingTiles.Clear();
                        InitialTile = null;
                    }
                }                           
            }           
        }

        for(int i = 0; i < MatchingTiles.Count; i++)
        {
            MatchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
        }
    }

}
