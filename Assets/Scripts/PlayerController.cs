using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int MaxMoves;
    private int MovesLeft;
    private GridManagerScript GMScript;
    private Text MoveText;
    private GameObject MoveTextHolder;

    // Start is called before the first frame update
    void Start()
    {
        MovesLeft = MaxMoves;
        MoveText = GameObject.Find("PlayerMovesText").GetComponent<Text>();

        MoveTextHolder = GameObject.Find("MoveTextHolder");
        MoveTextHolder.transform.localPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        MoveText.text = MovesLeft.ToString();
        GMScript = GameObject.Find("GridManager").GetComponent<GridManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //if player x is less than grid WIDTH then can move
            GMScript.MovePlayer("Right");
            CheckGameOver();
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // if player x is greater than 0, then can move
            GMScript.MovePlayer("Left");
            CheckGameOver();
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            // if player y is less than HEIGHT, then can move
            GMScript.MovePlayer("Up");
            CheckGameOver();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            // if player y is greater than 0, then can move
            GMScript.MovePlayer("Down");           
            CheckGameOver();
        }
        MoveTextHolder.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
    }

    void CheckGameOver()
    {
        MovesLeft--;
        MoveText.text = MovesLeft.ToString();

        if (MovesLeft < 1)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ResetMoves()
    {
        MovesLeft = MaxMoves;
        MoveText.text = MovesLeft.ToString();
    }
}








