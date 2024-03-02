using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chunk : MonoBehaviour
{
    [SerializeField] Station StationPrefab;

    public void GenerateChunk(Vector3 centerPosition,bool generateStation)
    {
        int width =  WorldManager.Instance.ChunkDimensions.x;
        int height = WorldManager.Instance.ChunkDimensions.y;
        int depth =  WorldManager.Instance.ChunkDimensions.z;

        Vector3 position = new Vector3(-width / 2, -height / 2, -depth / 2)/*-(centerPosition-ChunkDimensions/2)*/;

        VoxelArray voxels = new VoxelArray(width, height, depth);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    float u = Mathf.Lerp(-width / 2, width / 2, x / ((float)width));
                    float v = Mathf.Lerp(-height / 2, height / 2, y / ((float)height));
                    float w = Mathf.Lerp(-depth / 2, depth / 2, z / ((float)depth));

                    voxels[x, y, z] = WorldManager.Instance.worldSettings.GetNoise(new Vector3(u, v, w) + centerPosition);
                }
            }
        }

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        WorldManager.Instance.marching.Generate(voxels.Voxels, vertices, triangles);
        CreateMesh32(vertices, triangles, centerPosition);
        if (generateStation)
        {
            Instantiate(StationPrefab, centerPosition, Quaternion.identity, transform);
        }
        
    }

    private void CreateMesh32(List<Vector3> verts, List<int> indices, Vector3 position)
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = IndexFormat.UInt32;
        mesh.SetVertices(verts);
        mesh.SetTriangles(indices, 0);
        mesh.RecalculateNormals();

        mesh.RecalculateBounds();

        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;

        transform.localPosition = position;
    }
}
