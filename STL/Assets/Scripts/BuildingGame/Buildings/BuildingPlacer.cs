using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer Instance;

    public Building BuildingToPlace;

    Building buildingInstance;

    Plane groundPlane;

    public NavMeshSurface navMeshSurface;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetBuildingToPlace(Building building)
    {
        Debug.Log("I set my Building");
        BuildingToPlace = building;
        groundPlane = new Plane(Vector3.up, 0);

        buildingInstance = Instantiate(BuildingToPlace, transform.position, Quaternion.identity, transform);
    }

    public void PlaceBuildingToPlace()
    {
        buildingInstance.transform.parent = GridManager.Instance.Grid[GridUtil.WorlPosToGridCoord(transform.position, GridManager.Instance.tilePrefab.tileDimensions)].transform;
        buildingInstance.Place(true);
        navMeshSurface.BuildNavMesh();
        BuildingToPlace = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(BuildingToPlace == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float enterDistance))
        {
            if (GridManager.Instance.Grid.ContainsKey(GridUtil.WorlPosToGridCoord(ray.GetPoint(enterDistance), GridManager.Instance.tilePrefab.tileDimensions)))
            {
                transform.position = GridManager.Instance.Grid[GridUtil.WorlPosToGridCoord(ray.GetPoint(enterDistance), GridManager.Instance.tilePrefab.tileDimensions)].transform.position;
            }
            else
            {
                transform.position = ray.GetPoint(enterDistance);
            }
        }
    }
}
