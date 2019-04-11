using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int MaxMoves;
    private int MovesLeft;
    private GridManagerScript GMScript;

    // Start is called before the first frame update
    void Start()
    {
        MovesLeft = MaxMoves;
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
    }

    void CheckGameOver()
    {
        MovesLeft--;

        if (MovesLeft < 1)
        {
            // game over
        }
    }
}








