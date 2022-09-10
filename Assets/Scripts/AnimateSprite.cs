using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimateSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }

    public Sprite[] sprites;
    public float rate = 0.5f;
    public int sprite { get; private set; }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(NextAnimationFrame), rate, rate);   
    }

    void NextAnimationFrame()
    {
        sprite++;

        if(sprite >= sprites.Length)
        {
            sprite = 0;
        }

        spriteRenderer.sprite = sprites[sprite];
    }

    public void restart()
    {
        sprite = 0;
        spriteRenderer.sprite = sprites[sprite];
    }
}
