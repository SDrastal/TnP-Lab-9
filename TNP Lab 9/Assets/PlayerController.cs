using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float clampX = 18f;
    public float projectileSpeed = 10f;
    public float projectileSpawnOffset = 1.0f;
    public int StartingHealth = 100;

    private int currentHealth;

    private Rigidbody2D rb;
    private ProjectileObjectPool projectilePool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectilePool = FindAnyObjectByType<ProjectileObjectPool>();

        currentHealth = StartingHealth;

        gameObject.tag = "Player";
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(movement.x * speed, 0);

        float clampedX = Mathf.Clamp(transform.position.x, -clampX, clampX);
        transform.position = new Vector2(clampedX, transform.position.y);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject projectile = projectilePool.GetProjectile();
            if (projectile != null)
            {
                projectile.transform.position = transform.position + Vector3.up * projectileSpawnOffset;
                Projectile projScript = projectile.GetComponent<Projectile>();

                if (projScript != null)
                    projScript.Initialize(Vector2.up, projectileSpeed);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Debug.Log("Player has died.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
            if (enemy != null)
            {
                TakeDamage(enemy.damage);
            }
            Destroy(collision.gameObject);
        }
    }
}
