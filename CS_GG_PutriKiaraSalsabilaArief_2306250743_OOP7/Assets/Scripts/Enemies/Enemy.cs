using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;  
    public Sprite enemySprite; 
    private Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        if (rb != null)
        {
            rb.gravityScale = 0;   
        }

        if (enemySprite != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = enemySprite;
            }
        }
    }

    public virtual void Move() { }
}
