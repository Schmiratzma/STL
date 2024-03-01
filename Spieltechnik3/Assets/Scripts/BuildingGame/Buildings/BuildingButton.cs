using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] public BuildingDataSO buildingData;

    public void OnBeginDrag(PointerEventData eventData)
    {
        bool affordable = true;
        foreach (Cost res in buildingData.costs)
        {
            if (res.amount > GameManager.Instance.playerInventory.GetRessourceAmount(res.resource))
            {
                affordable = false;
            }
        }
        if (affordable)
        {
            BuildingPlacer.Instance.SetBuildingToPlace(buildingData.buildingPrefab);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = buildingData.BuildingImage;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BuildingPlacer.Instance.PlaceBuildingToPlace();
    }
}
