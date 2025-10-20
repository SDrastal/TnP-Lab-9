using UnityEngine;
using UnityEngine.Events;
using System;

public class IEnemy : MonoBehaviour
{
    public float speed;
    public int health;
    public int damage;
    public Sprite enemySprite;
    [HideInInspector]
    public int direction = 1;

    public UnityEvent onEdge;
    public UnityEvent onDestroyed;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        if (onEdge == null)
            onEdge = new UnityEvent();

        if (onDestroyed == null)
            onDestroyed = new UnityEvent();
            
        gameObject.tag = "Enemy";
        Move();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
            onEdge.Invoke();
        
        if (other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            onDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(speed * direction, 0);
    }
}
