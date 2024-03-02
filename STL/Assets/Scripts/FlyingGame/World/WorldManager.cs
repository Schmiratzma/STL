using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private Material material;

    [SerializeField] public WorldSettings worldSettings;

    [SerializeField] Vector3Int ChunkDimensions;

    [SerializeField] Vector2 NoiseThresholds;

    [SerializeField] float PointDensity;

    private List<GameObject> meshes = new List<GameObject>();

    private MarchingCubes marching;

    // Start is called before the first frame update
    void Start()
    {
        worldSettings.MakeNoise();

        //mby also get noise idk yet

        marching = new MarchingCubes(0f);

        //GenerateChunk(Vector3.zero);
        //GenerateChunk(Vector3.up * (ChunkDimensions.y - 1));
        //GenerateChunk(Vector3.down * (ChunkDimensions.y-1));
        //GenerateChunk(Vector3.forward *( ChunkDimensions.z-1));
        //GenerateChunk(Vector3.back * (ChunkDimensions.z-1));
        //GenerateChunk(Vector3.left * (ChunkDimensions.x-1));
        //GenerateChunk(Vector3.right * (ChunkDimensions.x-1));
        for(int x= -1; x <= 1; x++)
        {
            for(int y= -1; y <= 1; y++)
            {
                for(int z= -1; z <= 1; z++)
                {
                    GenerateChunk(new Vector3(x * (ChunkDimensions.x - 1), y * (ChunkDimensions.y - 1), z * (ChunkDimensions.z - 1)));
                }
            }
        }
    }

    public void GenerateChunk(Vector3 centerPosition)
    {
        int width = ChunkDimensions.x;
        int height = ChunkDimensions.y;
        int depth = ChunkDimensions.z;

        Vector3 position = new Vector3(-width / 2, -height / 2, -depth / 2)/*-(centerPosition-ChunkDimensions/2)*/;

        VoxelArray voxels = new VoxelArray(width, height, depth);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    float u = Mathf.Lerp(-width/2, width/2, x / ((float)width));
                    float v = Mathf.Lerp(-height/2, height/2, y / ((float)height));
                    float w = Mathf.Lerp(-depth/2, depth/ 2, z / ((float)depth));

                    voxels[x, y, z] = worldSettings.GetNoise(new Vector3(u, v, w)+centerPosition);
                }
            }
        }

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        marching.Generate(voxels.Voxels, vertices, triangles);      
        CreateMesh32(vertices, triangles, centerPosition);
    }

    private void CreateMesh32(List<Vector3> verts, List<int> indices, Vector3 position)
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = IndexFormat.UInt32;
        mesh.SetVertices(verts);
        mesh.SetTriangles(indices, 0);
        mesh.RecalculateNormals();

        mesh.RecalculateBounds();

        GameObject go = new GameObject("Mesh");
        go.transform.parent = transform;
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();
        go.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
        go.GetComponent<Renderer>().material = material;
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.transform.localPosition = position;

        meshes.Add(go);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
