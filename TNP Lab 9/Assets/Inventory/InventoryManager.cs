using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem[] items;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool[] usedIDs = new bool[9999];
        usedIDs[0] = true; // Reserve ID 0 as invalid
        items = FindObjectsByType<InventoryItem>(FindObjectsSortMode.None);
        foreach (InventoryItem item in items)
        {
            while (usedIDs[item.itemID])
                item.SetID(Random.Range(1000, 9999));

            usedIDs[item.itemID] = true;

            item.SetValue(Random.Range(0.0f, 999.99f));
        }
        foreach (InventoryItem item in items)
        {
            Debug.Log(item.ToString());
        }
        QuickSortByValue(0, items.Length - 1);
        foreach (InventoryItem item in items)
        {
            Debug.Log(item.ToString());
        }    
    }

    public InventoryItem LinearSearchByName(string name)
    {
        //Jadyn
        return null;
    }

    public InventoryItem BinarySearchByID(int id)
    {
        //Sam
        return null;
    }

    public void QuickSortByValue(int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(low, high);
            QuickSortByValue(low, pivotIndex);
            QuickSortByValue(pivotIndex + 1, high);
        }
    }

    //Hoare's Partition for quick sort
    private int Partition(int low, int high)
    {
        float pivot = items[low].itemValue;
        int i = low - 1, j = high + 1;
        while (true)
        {
            // find next element larger than pivot from the left
            do
            {
                i++;
            } while (i <= high && items[i].itemValue < pivot);

            // find next element smaller than pivot from the right
            do
            {
                j--;
            } while (j >= low && items[j].itemValue > pivot);

            // if left and right crosses each other no swapping required
            if (i >= j) return j;

            // swap larger and smaller elements
            Swap(i, j);
        }
    }

    private void Swap(int i, int j)
    {
        InventoryItem temp = items[i];
        items[i] = items[j];
        items[j] = temp;
    }
}