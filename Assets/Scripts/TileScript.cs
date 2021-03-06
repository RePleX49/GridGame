﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Sprite[] TileSprites;
    [HideInInspector] public int TileType;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        SetType();
    }

    public void SetType()
    {
        TileType = Random.Range(0, TileSprites.Length);
        sr.sprite = TileSprites[TileType];
    }

    public bool CompareTiles(GameObject t1, GameObject t2)
    {
        if(t1.gameObject != null && t2.gameObject != null && t1.CompareTag("Tile") && t2.CompareTag("Tile"))
        {
            SpriteRenderer ts1 = t1.GetComponent<SpriteRenderer>();
            SpriteRenderer ts2 = t2.GetComponent<SpriteRenderer>();

            return (ts1.sprite == sr.sprite && ts2.sprite == sr.sprite);
        }
        else
        {
            return false;
        }
    }
}
