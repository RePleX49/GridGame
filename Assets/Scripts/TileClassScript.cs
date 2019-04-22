using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClassScript : MonoBehaviour
{
    public int TileType;

    public ParticleSystem particleFX;
    public AudioSource audioSource;
    public SpriteRenderer sr;

    public virtual void ActivateEffect()
    {
        // activate some effect   
        audioSource.Play();
        particleFX.Play();
    }

    public bool CompareTiles(GameObject t1, GameObject t2)
    {
        if (t1.gameObject != null && t2.gameObject != null && t1.CompareTag("Tile") && t2.CompareTag("Tile"))
        {
            TileClassScript ts1 = t1.GetComponent<TileClassScript>();
            TileClassScript ts2 = t2.GetComponent<TileClassScript>();

            return (ts1.TileType == TileType && ts2.TileType == TileType);
        }
        else
        {
            return false;
        }
    }
}
