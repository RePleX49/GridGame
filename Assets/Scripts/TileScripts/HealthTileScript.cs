using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTileScript : TileClassScript
{
    public int HealthGiven = 2;

    override public void ActivateEffect() 
    {
        base.ActivateEffect();

        Debug.Log(particleFX.isPlaying);

        PlayerController.Instance.MovesLeft += HealthGiven;

        PlayerController.Instance.MoveText.text = "HP: " +  PlayerController.Instance.MovesLeft.ToString();
    }
}
