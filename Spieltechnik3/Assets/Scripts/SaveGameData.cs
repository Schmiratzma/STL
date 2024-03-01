using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{
    [SerializeField] public List<BuildingData> Buildings;
    [SerializeField] public List<UnitData> Units;
    [SerializeField] public List<InventoryEntry> playerInventory;
}
