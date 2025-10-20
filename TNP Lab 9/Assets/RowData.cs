using UnityEngine;
using System.Collections.Generic;

public class RowData : MonoBehaviour
{
    private static RowData instance;
    public static RowData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<RowData>();
            }
            return instance;
        }
    }

    [HideInInspector]
    public float topY = 12f;
    [HideInInspector]
    public float bottomY = -5.5f;
    [HideInInspector]
    public int numberOfRows = 5;

    [HideInInspector]
    public float[] rowYPositions;
    [HideInInspector]
    public int[] rowDirection;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        float rowHeight = (topY - bottomY) / (numberOfRows - 1);
        rowYPositions = new float[numberOfRows];
        rowDirection = new int[numberOfRows];
        for (int i = 0; i < numberOfRows; i++)
        {
            float yPos = topY - i * rowHeight;
            rowYPositions[i] = yPos;
            rowDirection[i] = (i % 2 == 0) ? 1 : -1; // Even rows move right, odd rows move left
        }
    }
}
