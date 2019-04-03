using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Sprite[] TileSprites;
    public int TileType;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        SetType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType()
    {
        TileType = Random.Range(0, TileSprites.Length);
        sr.sprite = TileSprites[TileType];
    }

    public bool CompareTiles(GameObject t1, GameObject t2)
    {
        TileScript ts1 = t1.GetComponent<TileScript>();
        TileScript ts2 = t2.GetComponent<TileScript>();

        return (ts1.TileType == TileType && ts2.TileType == TileType);
    }
}
