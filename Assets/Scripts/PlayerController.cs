using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int MaxMoves = 10;
    [HideInInspector] public int MovesLeft;
    [HideInInspector] public int AttacksLeft;
    [HideInInspector] public int GoldCollected = 0;

    public static PlayerController Instance = null;

    private GridManagerScript GMScript;
    public Text MoveText;
    public Text AttackText;

    [HideInInspector] public bool InputDisabled = false;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MovesLeft = MaxMoves;
        AttacksLeft = 12;

        MoveText.text = "HP: " +  MovesLeft.ToString();

        AttackText.text = "Attack: " + AttacksLeft.ToString();

        GMScript = GameObject.Find("GridManager").GetComponent<GridManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!InputDisabled)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //if player x is less than grid WIDTH then can move
                GMScript.MovePlayer("Right");
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // if player x is greater than 0, then can move
                GMScript.MovePlayer("Left");
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // if player y is less than HEIGHT, then can move
                GMScript.MovePlayer("Up");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // if player y is greater than 0, then can move
                GMScript.MovePlayer("Down");
            }           
        }
    }

    public void CheckGameOver()
    {
        MovesLeft--;
        MoveText.text = "HP: " + MovesLeft.ToString();

        if (MovesLeft < 1)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void AddMoves()
    {
        MovesLeft++;
        MoveText.text = "HP: " + MovesLeft.ToString();
    }
}








