using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileScript : TileClassScript
{
    public int EnemyDamage = 4;
    public int GoldAmount = 10;

    public override void ActivateEffect()
    {
        base.ActivateEffect();

        Debug.Log(particleFX.isPlaying);

        PlayerController.Instance.GoldCollected += GoldAmount;

        if(PlayerController.Instance.AttacksLeft >= EnemyDamage)
        {
            PlayerController.Instance.AttacksLeft -= EnemyDamage;
            PlayerController.Instance.AttackText.text = "Attacks: " + PlayerController.Instance.AttacksLeft;
        }
        else
        {
            PlayerController.Instance.MovesLeft -= EnemyDamage;
        }
        
        if(PlayerController.Instance.MovesLeft <= 0)
        {
            PlayerController.Instance.MovesLeft = 0;
            PlayerController.Instance.CheckGameOver();
        }
    }
}
