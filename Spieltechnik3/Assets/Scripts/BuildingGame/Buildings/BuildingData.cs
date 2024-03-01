using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BuildingData
{
    [SerializeField] public int iD;

    [SerializeField] public BuildingDataSO buildinDataSO;

    public Vector2Int buildingLocation;

    //public void UpdateBuildingData(Building building)
    //{
    //    Buildings.Add(building);
    //}
}
