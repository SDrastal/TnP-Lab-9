using UnityEngine;
using UnityEngine.Events;

public class IEnemy : MonoBehaviour
{
    public float speed;
    public int health;
    public int damage;

    public float leftEdge;
    public float rightEdge;
    public UnityEvent onEdge;

    private int row = 0;

}
