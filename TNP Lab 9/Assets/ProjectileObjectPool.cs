using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int poolSize = 50;

    private Queue<GameObject> projectilePool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        projectilePool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.SetParent(transform);
            projectilePool.Enqueue(projectile);
            projectile.SetActive(false);
        }
    }

    public GameObject GetProjectile()
    {
        if (projectilePool.Count > 0)
        {
            GameObject projectile = projectilePool.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            Debug.LogWarning("Projectile pool is empty!");
            return null;
        }
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectilePool.Enqueue(projectile);
        projectile.SetActive(false);
    }
}
