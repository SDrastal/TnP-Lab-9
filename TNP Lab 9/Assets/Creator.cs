using UnityEngine;

public abstract class Creator : MonoBehaviour
{
    public abstract GameObject FactoryMethod(GameObject enemyPrefab);
}
