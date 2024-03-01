using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cost
{
    public Ressource resource;
    public int amount;
}

public class Building : MonoBehaviour
{
    [SerializeField] public Vector2Int coordinate;

    [SerializeField] public BuildingDataSO buildingDataSO;


    public void Place(bool withPlaceEffect)
    {
        foreach (Cost res in buildingDataSO.costs)
        {
            GameManager.Instance.playerInventory.ChangeAmount(res.resource, -1 * res.amount);
            UIManager.Instance.UpdateInventoryUI(res.resource);
        }
        coordinate = GridUtil.WorlPosToGridCoord(transform.position, GridManager.Instance.tilePrefab.tileDimensions);

        if(!withPlaceEffect)
        {
            return;
        }
        foreach (Effect effect in buildingDataSO.onPlacementEffects)
        {
            effect.TriggerEffect();
        }
    }
}
