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

    public event Action onEdge;
    public event Action onDestroyed;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private ProjectileObjectPool projectilePool;


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        projectilePool = FindAnyObjectByType<ProjectileObjectPool>();

        gameObject.tag = "Enemy";
        Move();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Edge"))
        {
            onEdge?.Invoke();
            Debug.Log("Enemy reached edge.");
        }
        if (other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(1);
            projectilePool.ReturnProjectile(other.gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            onDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(speed * direction, 0);
    }
}
