using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int MaxMoves;
    private int MovesLeft;

    // Start is called before the first frame update
    void Start()
    {
        MovesLeft = MaxMoves;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //if player x is less than grid WIDTH then can move
            MovesLeft--;
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // if player x is greater than 0, then can move
            MovesLeft--;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            // if player y is less than HEIGHT, then can move
            MovesLeft--;
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            // if player y is greater than 0, then can move
            MovesLeft--;
        }
    }
}








