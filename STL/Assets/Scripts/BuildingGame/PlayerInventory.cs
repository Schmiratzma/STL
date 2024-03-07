using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<InventoryEntry> inventoryEntries;

    public List<InventoryEntry> MyInventoryEntries
    {
        get { return inventoryEntries; }
        set { inventoryEntries = value; }
    }

    public int GetRessourceAmount(Ressource ressource)
    {
        int result = 0;
        foreach (InventoryEntry entry in inventoryEntries)
        {
            if (entry.RessourceType == ressource)
            {
                result += entry.amount;
            }
        }
        return result;
    }

    public void ChangeAmount(Ressource ressource, int amount)
    {
        foreach (InventoryEntry entry in inventoryEntries)
        {
            if (ressource == entry.RessourceType)
            {
                if (entry.amount + amount > entry.Capacity)
                {
                    entry.amount = entry.Capacity;
                }
                else
                {
                    entry.amount += amount;
                }

                return;
            }
        }
        Debug.LogError("Ressource type not found in PlayerInventory");
    }

    public void restartInventory(List<InventoryEntry> newInventoryEntries)
    {
        if (inventoryEntries == null)
        {
            inventoryEntries = new List<InventoryEntry>();
        }
        inventoryEntries.Clear();
        inventoryEntries.AddRange(newInventoryEntries);
    }

    public void ChangeCapacity(Ressource ressource, int capacity)
    {
        foreach (InventoryEntry entry in inventoryEntries)
        {
            if (entry.RessourceType == ressource)
            {
                entry.Capacity += capacity;
            }
        }
        UIManager.Instance.UpdateCompleteInventoryUI();
    }

    public int GetCapacity(Ressource ressource)
    {
        foreach(InventoryEntry entry in inventoryEntries)
        {
            if(entry.RessourceType == ressource)
            {
                return entry.Capacity;
            }
        }
        Debug.LogWarning("no Entry of this Ressource found to return a capacity for it.");
        return 0;
    }

}
