using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTileScript : TileClassScript
{
    override public void ActivateEffect() 
    {
        GameObject Player = GameObject.Find("Character");
        Player.GetComponent<PlayerController>().MovesLeft += 2;
    }
}
