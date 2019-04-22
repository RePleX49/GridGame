using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManagerScript : MonoBehaviour
{
    public static GridManagerScript Instance;

    // Always use even number for WIDTH and HEIGHT or you will break the game
    private const int WIDTH = 6;
    private const int HEIGHT = 8;

    [SerializeField] private GameObject[,] TileGrid;
    [SerializeField] private GameObject ActivePlayer;
    private AudioSource audioSource;

    public GameObject[] TilePrefab;
    public CameraShake cameraShake;
    public Text ScoreText;

    [HideInInspector] public int TotalScore;
    [HideInInspector] public bool ObjectIsMoving;

    bool running = false;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        TileGrid = new GameObject[WIDTH, HEIGHT];
        MakeGrid();
        CenterCamera();

        TileClassScript ts = CheckMatches();
        while (ts != null)
        {
            int XPos = (int)ts.gameObject.transform.position.x;
            int YPos = (int)ts.gameObject.transform.position.y;

            Destroy(TileGrid[XPos, YPos]);
            TileGrid[XPos, YPos] = null;

            GameObject tile = Instantiate(TilePrefab[Random.Range(0, TilePrefab.Length)]);
            tile.transform.SetParent(this.transform);
            tile.transform.localPosition = new Vector2(XPos, YPos);

            TileGrid[XPos, YPos] = tile;
            ts = CheckMatches();
        }

        // Clear a spot in grid for player
        Destroy(TileGrid[WIDTH / 2, HEIGHT / 2]);

        ActivePlayer.transform.localPosition = new Vector2(WIDTH / 2, HEIGHT / 2);
        TileGrid[WIDTH / 2, HEIGHT / 2] = ActivePlayer;      
    }

    void MakeGrid()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                GameObject tile = Instantiate(TilePrefab[Random.Range(0, TilePrefab.Length)]);
                tile.transform.SetParent(this.transform);
                tile.transform.localPosition = new Vector2(x, y);

                TileGrid[x, y] = tile;
            }
        }
    }

    void CenterCamera()
    {
        Vector2 MiddleTilePos = TileGrid[WIDTH / 2, HEIGHT / 2].transform.position;
        Vector3 NewCameraPos = new Vector3(MiddleTilePos.x - 0.5f, MiddleTilePos.y - 0.5f, -10);
        Camera.main.gameObject.transform.position = NewCameraPos;
        cameraShake.GetInitialPos(NewCameraPos);
    }

    // used to prevent matches in initial board setup
    public TileClassScript CheckMatches()
    {
        for(int x = 0; x < WIDTH; x++) {
            for(int y = 0; y < HEIGHT; y++) {
                if (TileGrid[x, y] != null && TileGrid[x, y].CompareTag("Tile"))
                {
                    TileClassScript ts = TileGrid[x, y].GetComponent<TileClassScript>();

                    if (x < WIDTH - 2 && ts.CompareTiles(TileGrid[x + 1, y], TileGrid[x + 2, y]))
                    {
                        return ts;
                    }
                    else if (y < HEIGHT - 2 && ts.CompareTiles(TileGrid[x, y + 1], TileGrid[x, y + 2]))
                    {
                        return ts;
                    }
                }                   
            }
        }

        return null;
    }

    public void MovePlayer(string Direction)
    {
        if(!ObjectIsMoving)
        {
            int PlayerX = (int)ActivePlayer.transform.position.x;
            int PlayerY = (int)ActivePlayer.transform.position.y;

            switch (Direction)
            {
                case "Right":
                    if (PlayerX < WIDTH)
                    {
                        TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX + 1, PlayerY];
                        StartCoroutine(SmoothTranslation(TileGrid[PlayerX, PlayerY], TileGrid[PlayerX, PlayerY].transform.position, 
                            new Vector2(PlayerX, PlayerY), 0.1f));

                        TileGrid[PlayerX + 1, PlayerY] = ActivePlayer;
                        StartCoroutine(SmoothTranslation(ActivePlayer, ActivePlayer.transform.position, 
                            new Vector2(PlayerX + 1, PlayerY), 0.1f));
                    }

                    break;

                case "Left":
                    if (PlayerX > 0)
                    {
                        TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX - 1, PlayerY];
                        StartCoroutine(SmoothTranslation(TileGrid[PlayerX, PlayerY], TileGrid[PlayerX, PlayerY].transform.position, 
                            new Vector2(PlayerX, PlayerY), 0.1f));

                        TileGrid[PlayerX - 1, PlayerY] = ActivePlayer;
                        StartCoroutine(SmoothTranslation(ActivePlayer, ActivePlayer.transform.position, 
                            new Vector2(PlayerX - 1, PlayerY), 0.1f));
                    }

                    break;

                case "Up":
                    if (PlayerY < HEIGHT)
                    {
                        TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX, PlayerY + 1];
                        StartCoroutine(SmoothTranslation(TileGrid[PlayerX, PlayerY], TileGrid[PlayerX, PlayerY].transform.position, 
                            new Vector2(PlayerX, PlayerY), 0.1f));

                        TileGrid[PlayerX, PlayerY + 1] = ActivePlayer;
                        StartCoroutine(SmoothTranslation(ActivePlayer, ActivePlayer.transform.position, 
                            new Vector2(PlayerX, PlayerY + 1), 0.1f));
                    }

                    break;

                case "Down":
                    if (PlayerY > 0)
                    {
                        TileGrid[PlayerX, PlayerY] = TileGrid[PlayerX, PlayerY - 1];
                        StartCoroutine(SmoothTranslation(TileGrid[PlayerX, PlayerY], TileGrid[PlayerX, PlayerY].transform.position, 
                            new Vector2(PlayerX, PlayerY), 0.1f));

                        TileGrid[PlayerX, PlayerY - 1] = ActivePlayer;
                        StartCoroutine(SmoothTranslation(ActivePlayer, ActivePlayer.transform.position, 
                            new Vector2(PlayerX, PlayerY - 1), 0.1f));
                    }

                    break;

                default:
                    break;
            }

            StartCoroutine(ClearMatches());

            TotalScore = PlayerController.Instance.GoldCollected;
            PlayerController.Instance.CheckGameOver();
        }

        
    }

    IEnumerator ClearMatches()
    {
        yield return new WaitForSeconds(0.12f);

        List<GameObject> MatchingTiles = new List<GameObject>();

        for (int x = 0; x < WIDTH; x++){ 
            for (int y = 0; y < HEIGHT; y++){

                //Check tag to make sure that we don't try to access the player "tile"
                // REMEMBER TO ALWAYS CHECK NULL FIRST
                if(TileGrid[x, y] != null && TileGrid[x, y].CompareTag("Tile"))
                {
                    TileClassScript ts = TileGrid[x, y].GetComponent<TileClassScript>();

                    if (x < WIDTH - 2 && ts.CompareTiles(TileGrid[x + 1, y], TileGrid[x + 2, y]))
                    {
                        //Check if tiles are already in List to prevent duplicate
                        if(!MatchingTiles.Contains(TileGrid[x, y]))
                        {
                            MatchingTiles.Add(TileGrid[x, y]);
                        }
                        
                        if(!MatchingTiles.Contains(TileGrid[x + 1, y]))
                        {
                            MatchingTiles.Add(TileGrid[x + 1, y]);
                        }
                        
                        if(!MatchingTiles.Contains(TileGrid[x + 2, y]))
                        {
                            MatchingTiles.Add(TileGrid[x + 2, y]);
                        }                       
                    }
                    else if (y < HEIGHT - 2 && ts.CompareTiles(TileGrid[x, y + 1], TileGrid[x, y + 2]))
                    {
                        //Check if tiles are already in List to prevent duplicate
                        if (!MatchingTiles.Contains(TileGrid[x, y]))
                        {
                            MatchingTiles.Add(TileGrid[x, y]);
                        }
                        
                        if(!MatchingTiles.Contains(TileGrid[x, y + 1]))
                        {
                            MatchingTiles.Add(TileGrid[x, y + 1]);
                        }
                        
                        if(!MatchingTiles.Contains(TileGrid[x, y + 2]))
                        {
                            MatchingTiles.Add(TileGrid[x, y + 2]);
                        }                      
                    }                 
                }

                // Debug.Log("Looping Through: " + y);
            }
        }

        //Debug.Log("MatchingTile Count: " + MatchingTiles.Count);       

        if(MatchingTiles.Count >= 3 && !running)
        {
            running = true;
            PlayerController.Instance.InputDisabled = true;                  

            StartCoroutine(cameraShake.Shake(0.2f, 0.2f));

            // Remove matched tiles
            for (int i = 0; i < MatchingTiles.Count; i++)
            {
                MatchingTiles[i].GetComponent<TileClassScript>().ActivateEffect();    
            }

            yield return new WaitForSeconds(0.65f);

            for (int i = 0; i < MatchingTiles.Count; i++)
            {
                Destroy(MatchingTiles[i]);
            }

            running = false;

            PlayerController.Instance.AddMoves();

            ScoreText.text = "Gold: " + PlayerController.Instance.GoldCollected.ToString();

            
        }

        

        Debug.Log(MatchingTiles.Count);

        StartCoroutine(ShiftTilesDown());
    }

    IEnumerator ShiftTilesDown()
    {
        //TODO figure out why this works
        yield return new WaitForEndOfFrame();

        int nullCount = 0;

        for (int x = 0; x < WIDTH; x++)
        {
            bool PlayerInColumn = false;
            for (int y = 0; y < HEIGHT; y++)
            {
                if (TileGrid[x, y] == null || TileGrid[x, y] == ActivePlayer)
                {
                    nullCount++;

                    if (TileGrid[x, y] == ActivePlayer)
                    {
                        PlayerInColumn = true;
                    }
                }
                else if(nullCount > 0)
                {
                    if (!(PlayerInColumn && nullCount < 2))
                    {
                        if (TileGrid[x, y].CompareTag("Tile"))
                        {
                            int NewX = (int)TileGrid[x, y].transform.position.x;
                            int NewY = (int)TileGrid[x, y].transform.position.y - nullCount;

                            if (NewY < HEIGHT)
                            {
                                if (TileGrid[NewX, NewY] != null)
                                {
                                    NewY += 1;
                                    if (NewY > WIDTH)
                                    {
                                        Destroy(TileGrid[x, y]);

                                        yield break;
                                        //TileGrid[x, y] = null;
                                        //yield return null;
                                    }
                                }
                            }
                            else
                            {
                                yield break;
                            }

                            StartCoroutine(SmoothTranslation(TileGrid[x, y], TileGrid[x, y].transform.position, 
                                new Vector2(NewX, NewY), 0.2f));

                            TileGrid[NewX, NewY] = TileGrid[x, y];
                            TileGrid[x, y] = null;
                        }
                    }
                }
            }
            nullCount = 0;
        }

        yield return new WaitForSeconds(0.75f);
        
        StartCoroutine(RefillBoard());
    }

    IEnumerator RefillBoard()
    {
        for(int x = 0; x < WIDTH; x++)
        {
            for(int y = 0; y < HEIGHT; y++)
            {
                if(TileGrid[x, y] == null)
                {
                    GameObject tile = Instantiate(TilePrefab[Random.Range(0, TilePrefab.Length)]);
                    tile.transform.SetParent(this.transform);
                    tile.transform.localPosition = new Vector2(x, HEIGHT + 3);

                    StartCoroutine(SmoothTranslation(tile, tile.transform.position, new Vector2(x, y), 0.225f));

                    TileGrid[x, y] = tile;
                }
            }
        }

        yield return new WaitForSeconds(0.7f);

        TileClassScript ts = CheckMatches();

        if (ts == null)
        {
            PlayerController.Instance.InputDisabled = false;
            yield break;
        }
        else if(ts != null)
        {
            StartCoroutine(ClearMatches());
        }

    }

    IEnumerator SmoothTranslation(GameObject MoveObject, Vector2 InitialPos, Vector2 TargetPos, float MoveDuration)
    {
        float InitialTime = Time.time;

        ObjectIsMoving = true;

        while(Time.time < InitialTime + MoveDuration && MoveObject != null)
        {
            MoveObject.transform.position = Vector2.Lerp(InitialPos, TargetPos, (Time.time - InitialTime) / MoveDuration);
            yield return null;
        }

        if(MoveObject != null)
        {
            MoveObject.transform.position = TargetPos;
        }

        ObjectIsMoving = false;

        yield return null;
    }
}
