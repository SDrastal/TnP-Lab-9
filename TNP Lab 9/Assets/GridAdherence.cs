using UnityEngine;
using System.Collections.Generic;
public class GridAdherence : MonoBehaviour
{
    [HideInInspector]
    public RowData rowData;
    [HideInInspector]
    public int currentRow = 0;
    [HideInInspector]
    public IEnemy enemy;

    void Awake()
    {
        rowData = RowData.Instance;
        if (rowData == null)
        {
            Debug.LogError("RowData instance not found in the scene.");
            return;
        }
        currentRow = 0;
        enemy = GetComponent<IEnemy>();
        enemy.onEdge += MoveDownRow;
    }

    void MoveDownRow()
    {
        currentRow++;
        if (currentRow < rowData.numberOfRows)
        {
            Vector2 position = transform.position;
            position.y = rowData.rowYPositions[currentRow];
            transform.position = position;
        }
        enemy.direction = rowData.rowDirection[currentRow];
        enemy.Move();
    }
}
