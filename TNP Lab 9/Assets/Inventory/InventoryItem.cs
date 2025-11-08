using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public int itemID { get; private set; }
    public string itemName { get; private set; }
    public float itemValue  { get; private set; }

    void Start()
    {
        itemName = gameObject.name;
    }

    public InventoryItem(int id, string name, float value)
    {
        itemID = id;
        itemName = name;
        itemValue = value;
    }

    public override string ToString()
    {
        return $"ID: {itemID}, Name: {itemName}, Value: {itemValue}";
    }

    public void SetID(int id)
    {
        itemID = id;
    }

    public void SetValue(float value)
    {
        itemValue = value;
    }
}
