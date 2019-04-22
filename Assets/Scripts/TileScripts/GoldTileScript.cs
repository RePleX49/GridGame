﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTileScript : TileClassScript
{
    public override void ActivateEffect()
    {
        base.ActivateEffect();

        PlayerController.Instance.GoldCollected += 5;
    }
}
