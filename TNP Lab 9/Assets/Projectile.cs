using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float speed)
    {
        this.speed = speed;
        rb.linearVelocity = direction.normalized * speed;
    }
}
