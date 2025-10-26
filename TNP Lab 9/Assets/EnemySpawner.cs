using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private RowData rowData;
    public int points = 0;
    private TransformSaver transformSaver; // Added

    void Awake()
    {
        rowData = FindAnyObjectByType<RowData>();
        transformSaver = FindAnyObjectByType<TransformSaver>(); // Added

        if (GetComponent<Enemy1Creator>() == null)
            gameObject.AddComponent<Enemy1Creator>();
        if (GetComponent<Enemy2Creator>() == null)
            gameObject.AddComponent<Enemy2Creator>();
        if (GetComponent<Enemy3Creator>() == null)
            gameObject.AddComponent<Enemy3Creator>();
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5f, 3f);
    }

    void SpawnEnemy()
    {
        Creator[] creators = GetComponents<Creator>();
        int randomIndex = Random.Range(0, creators.Length);

        Creator selectedCreator = creators[randomIndex];
        GameObject enemyObject = selectedCreator.FactoryMethod(enemyPrefabs[randomIndex]);
        IEnemy enemy = enemyObject.GetComponent<IEnemy>();
        enemy.onDestroyed += AddPoints;
        enemyObject.transform.position = new Vector2(Random.Range(-8f, 8f), 12f);

        // Register the enemy's transform for saving
        if (transformSaver != null)
            transformSaver.enemyTransforms.Add(enemyObject.transform);
    }

    void AddPoints()
    {
        points += 10;
        Debug.Log("Points: " + points);
    }

    public int Points
    {
        get { return points; }
        set { points = value; }
    }
}
