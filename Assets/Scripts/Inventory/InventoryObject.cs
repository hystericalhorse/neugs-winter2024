using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount)
    {
        bool hasItem = false;
        for(int i = 0; i < container.Count; i++)
        {
            if (container[i].item == item)
            {
                container[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            container.Add(new InventorySlot(item, amount));
        }
    }

    public void RemoveItem(ItemObject item, int amount)
    {
        bool hasItem = true;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].item == item)
            {
                container[i].RemoveAmount(amount);
                hasItem = false;
                if (!hasItem)
                {
                    container.RemoveAt(i);
                }
                break;
            }
        }

    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }   

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value) 
    {
        amount -= value;
    }
}
