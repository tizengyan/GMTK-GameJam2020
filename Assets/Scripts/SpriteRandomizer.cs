using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprite;

    public int arraySize;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        int i = (int)Random.Range(0f, arraySize);

        spriteRenderer.sprite = sprite[i];
    }

}
