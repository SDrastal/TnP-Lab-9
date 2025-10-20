using UnityEngine;

public class Enemy1Creator : Creator
{
    public override GameObject FactoryMethod(GameObject enemyPrefab)
    {
        GameObject enemy1 = Instantiate(enemyPrefab);
        return enemy1;
    }
}
