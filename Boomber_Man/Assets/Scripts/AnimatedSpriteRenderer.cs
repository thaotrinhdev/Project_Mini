using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite[] animationSrites;

    public float animationTime = 0.25f;
    private int animationFrame;

    public bool loop = true;
    public bool idle = true;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }
    private void NextFrame()
    {
        animationFrame++;
        if(loop && animationFrame >= animationSrites.Length) 
        {
             animationFrame = 0;
        }
        if(idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (animationFrame>=0 && animationFrame< animationSrites.Length)
        {
            spriteRenderer.sprite = animationSrites[animationFrame];
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Boom"))
        {
            other.isTrigger = false;
        }
    }
}
