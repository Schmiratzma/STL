using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] public NavMeshSurface navMeshSurface;

    [SerializeField] public GridTile tilePrefab;

    [SerializeField] Vector2Int GridDimensions;

    [SerializeField] List<Material> tileMaterials;

    public Dictionary<Vector2Int, GridTile> Grid;


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
        Grid = new Dictionary<Vector2Int, GridTile>();
        for(int x = -1* GridDimensions.x / 2 ;x < GridDimensions.x /2; x++)
        {
            for(int z = -1* GridDimensions.y / 2; z < GridDimensions.y /2; z++)
            {
                GridTile newTile = Instantiate(tilePrefab, transform);
                newTile.transform.position = GridUtil.GridCoordToWorldPos(new Vector2Int(x, z), tilePrefab.tileDimensions);
                newTile.GetComponentInChildren<MeshRenderer>().material = tileMaterials[(Mathf.Abs(x) + Mathf.Abs(z)) % 2];
                Grid.Add(new Vector2Int(x, z), newTile);
            }
        }
    }
}
