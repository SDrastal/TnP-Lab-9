using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private RowData rowData;
    private int points = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rowData = RowData.Instance;

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
    }

    void AddPoints()
    {
        points += 10;
        Debug.Log("Points: " + points);
    }
}
