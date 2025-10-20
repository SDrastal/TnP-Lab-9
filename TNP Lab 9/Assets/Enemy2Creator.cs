using UnityEngine;

public class Enemy2Creator : Creator
{
    public override GameObject FactoryMethod(GameObject enemyPrefab)
    {
        GameObject enemy2 = Instantiate(enemyPrefab);
        return enemy2;
    }
}
