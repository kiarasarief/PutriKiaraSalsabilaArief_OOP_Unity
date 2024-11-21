//Putri Kiara Salsabila Arief (2306250743)
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]public int level;  
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