using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTileScript : TileClassScript
{
    public override void ActivateEffect()
    {
        GameObject Player = GameObject.Find("Character");
        Player.GetComponent<PlayerController>().GoldCollected += 5;
    }
}
