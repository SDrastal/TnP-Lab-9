using UnityEngine;

public class Enemy3Creator : Creator
{
    public override GameObject FactoryMethod(GameObject enemyPrefab)
    {
        GameObject enemy3 = Instantiate(enemyPrefab);
        return enemy3;
    }
}
