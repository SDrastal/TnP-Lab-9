using UnityEngine;

public class GridAdherence : MonoBehaviour
{
    private float[][] rowsY;
    private int currentRow = 0;

    void Start()
    {
        for (int i = 0; i < rowsY.Length; i += 2)
        currentRow = 0;
        GetComponent<IEnemy>().onEdge.AddListener(MoveDownRow);
    }

    void MoveDownRow()
    {
        currentRow++;
        if (currentRow < rowsY.Length)
        {
            Vector3 position = transform.position;
            position.y = rowsY[currentRow];
            transform.position = position;
        }
    }
}
