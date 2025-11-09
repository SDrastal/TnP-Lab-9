using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> items;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool[] usedIDs = new bool[9999];
        usedIDs[0] = true; // Reserve ID 0 as invalid
        items = new List<InventoryItem>(FindObjectsByType<InventoryItem>(FindObjectsSortMode.None));
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
        Debug.Log("---- After Sorting by Value ----");
        QuickSortByValue(0, items.Count - 1);
        foreach (InventoryItem item in items)
        {
            Debug.Log(item.ToString());
        }    
    }

    public InventoryItem LinearSearchByName(string name)
    {
        foreach(InventoryItem item in items)
        {
            if (item.itemName == name)
                return item;
        }
        return null;
    }

    public InventoryItem BinarySearchByID(int id)
    {
        items.Sort((a, b) => a.itemID.CompareTo(b.itemID));

        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (items[mid].itemID == id)
                return items[mid];
            else if (items[mid].itemID < id)
                left = mid + 1;
            else
                right = mid - 1;
        }
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

    public void AddItemToInventory(int id, string name, int value)
    {
        InventoryItem newItem = new(id, name, value);
        items.Add(newItem);
    }

    public float CalculateTotalInventoryValue()
    {
        float totalValue = 0;
        foreach (InventoryItem item in items)
        {
            totalValue += item.itemValue;
        }
        return totalValue;
    }

    public List<InventoryItem> FilterItemsByValueRange(float minValue, float maxValue)
    {
        List<InventoryItem> filteredItems = new List<InventoryItem>();
        int lowerIndex = 0;
        int upperIndex = items.Count - 1;

        while (items[lowerIndex].itemValue < minValue)
        {
            lowerIndex++;
        }

        while (items[upperIndex].itemValue > maxValue)
        {
            upperIndex--;
        }

        for (int i = lowerIndex; i <= upperIndex; i++)
        {
            filteredItems.Add(items[i]);
        }
        return filteredItems;
    }
}