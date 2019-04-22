using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureTileScript : TileClassScript
{
    public override void ActivateEffect()
    {
        base.ActivateEffect();

        Debug.Log(particleFX.isPlaying);

        PlayerController.Instance.GoldCollected += 15;
    }
}
