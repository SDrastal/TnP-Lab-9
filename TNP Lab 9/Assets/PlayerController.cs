using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
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
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(movement.x * speed, 0);
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
                {
                    projScript.Initialize(Vector2.up, projectileSpeed);
                }
            }
        }
    }
}
