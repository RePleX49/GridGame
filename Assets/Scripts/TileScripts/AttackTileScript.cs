using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTileScript : TileClassScript
{
    public int AttackGiven = 3;

    override public void ActivateEffect()
    {
        base.ActivateEffect();

        Debug.Log(particleFX.isPlaying);

        PlayerController.Instance.AttacksLeft += AttackGiven;

        PlayerController.Instance.AttackText.text = "Attacks: " + PlayerController.Instance.AttacksLeft;
        Debug.Log("Attacks: " + PlayerController.Instance.AttacksLeft);
    }
}
