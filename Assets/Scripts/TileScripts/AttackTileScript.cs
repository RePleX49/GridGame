using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTileScript : TileClassScript
{
    override public void ActivateEffect()
    {
        GameObject Player = GameObject.Find("Character");
        Player.GetComponent<PlayerController>().AttacksLeft += 1;
    }
}
