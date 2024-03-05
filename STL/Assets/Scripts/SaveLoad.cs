using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using UnityEngine.AI;
using System.Linq;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] SaveGameData saveData;
    [SerializeField] string jsonString;

    [SerializeField] List<Building> buildingPrefabs;
    [SerializeField] List<Unit> unitPrefabs;

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        saveData = new SaveGameData();

        saveData.Buildings = new List<BuildingData>();
        Building[] buildings = GameObject.FindObjectsOfType<Building>();
        foreach (Building building in buildings)
        {
            BuildingData buildingData = new BuildingData();
            buildingData.buildingLocation = building.coordinate;
            buildingData.buildinDataSO = building.buildingDataSO;
            saveData.Buildings.Add(buildingData);
        }


        saveData.Units = new List<UnitData>();
        Unit[] units = GameObject.FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            UnitData unitData = new UnitData();
            unitData.ID = unit.unitData.ID;
            unitData.currentLocation = unit.transform.position;
            unitData.targetLocation = unit.GetComponent<NavMeshAgent>().destination;
            saveData.Units.Add(unitData);
        }

        saveData.playerInventory = GameManager.Instance.playerInventory.MyInventoryEntries;


        jsonString = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(getSavePath(), jsonString);
    }

    public void Load()
    {
        // check whether savefile exists or not
        if (!File.Exists(getSavePath()))
        {
            Debug.LogWarning("No SaveFile to load");
            return;
        }

        // convert savefile.json into Object of type SaveData
        jsonString = File.ReadAllText(getSavePath());
        saveData = JsonUtility.FromJson<SaveGameData>(jsonString);

        // Detroy all currently existing buildings
        Building[] buildings = FindObjectsOfType<Building>();
        foreach (Building building in buildings)
        {
            Destroy(building.gameObject);
        }

        // Instantiate all building from SaveData
        foreach (BuildingData buildingData in saveData.Buildings)
        {
            Building targetPrefab = buildingPrefabs.FirstOrDefault(x => x.buildingDataSO == buildingData.buildinDataSO);
            if (targetPrefab == null)
            {
                Debug.Log("no building prefab of type found");
                continue;
            }

            Vector3 spawnPoint = GridUtil.GridCoordToWorldPos(buildingData.buildingLocation, GridManager.Instance.tilePrefab.tileDimensions);
            Building newBuilding = Instantiate(targetPrefab, spawnPoint, Quaternion.identity);
            newBuilding.coordinate = buildingData.buildingLocation;
            newBuilding.buildingDataSO = buildingData.buildinDataSO;
        }

        // rebuild the navmesh
        GridManager.Instance.navMeshSurface.BuildNavMesh();

        Unit[] units = FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            Destroy(unit.gameObject);
        }

        foreach (UnitData unitData in saveData.Units)
        {
            Unit targetPrefab = unitPrefabs.FirstOrDefault(x => x.unitData.ID == unitData.ID);
            if (targetPrefab == null)
            {
                Debug.Log("no unit prefab of type " + unitData + "found");
                continue;
            }

            Vector3 spawnPoint = unitData.currentLocation;
            Unit newUnit = Instantiate(targetPrefab, spawnPoint, Quaternion.identity);
            newUnit.SetMovementTarget(unitData.targetLocation);
            newUnit.unitData = unitData;
        }

        GameManager.Instance.playerInventory.restartInventory(saveData.playerInventory);
        UIManager.Instance.UpdateCompleteInventoryUI();
    }

    private string getSavePath()
    {
        return Path.Combine(Application.persistentDataPath,"SaveGame.json");
    }

}
