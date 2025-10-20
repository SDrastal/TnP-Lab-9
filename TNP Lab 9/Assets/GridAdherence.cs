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
        currentRow = 0;
        enemy = GetComponent<IEnemy>();
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
