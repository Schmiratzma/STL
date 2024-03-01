using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BuildingData",menuName ="BuildingData")]
public class BuildingDataSO : ScriptableObject
{
    [SerializeField] public string buildingName;

    [SerializeField] public Sprite BuildingImage;

    [SerializeField] public List<Cost> costs;

    [SerializeField] public Building buildingPrefab;

    [SerializeField] public List<Effect> onPlacementEffects;
    
}
