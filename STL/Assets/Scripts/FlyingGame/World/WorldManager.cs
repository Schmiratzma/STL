using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using System.Linq;

public enum ChunkDirections
{
    downLeftback, downCenterback, downRightback,
    downLeftCenter, downCenterCenter, downRightCenter,
    downLeftForward, downCenterForward, downRightForward,
    centerLeftBack, centerCenterBack, centerRightBack,
    centerLeftCenter, centerCenterCenter, CenterRightCenter,
    CenterLeftForward, CenterCenterForward, CenterRightForward,
    upLeftBack, upCenterBack, upRightBack,
    upLeftCenter, upCenterCenter, upRightCenter,
    upLeftForward, upCenterForward, upRightForward
}

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    [SerializeField] private Material material;

    [SerializeField] public WorldSettings worldSettings;

    [SerializeField] public Vector3Int ChunkDimensions;

    [SerializeField] Vector2 NoiseThresholds;

    [SerializeField] float PointDensity;

    private List<GameObject> meshes = new List<GameObject>();

    public MarchingCubes marching;

    [SerializeField] public Chunk ChunkPrefab;

    public Dictionary<Vector3Int, Chunk> Chunks;

    private void Awake()
    {
        if (Instance == null)
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
        Chunks = new Dictionary<Vector3Int, Chunk>();
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
        int chungus = 0;
        for (int y = -1; y <= 1; y++)
        {
            for (int z = -1; z <= 1; z++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    Chunk newChunk = Instantiate(ChunkPrefab, transform);
                    newChunk.GenerateChunk(new Vector3(x * (ChunkDimensions.x - 1), y * (ChunkDimensions.y - 1), z * (ChunkDimensions.z - 1)),false);

                    Chunks.Add(new Vector3Int(x, y, z), newChunk);

                    //Chunks.Add((ChunkDirections)chungus, newChunk);
                    Debug.Log(new Vector3Int(x, y, z));
                    chungus++;
                    //GenerateChunk(new Vector3(x * (ChunkDimensions.x - 1), y * (ChunkDimensions.y - 1), z * (ChunkDimensions.z - 1)));
                }
            }
        }
        List<Chunk> toRemove = Chunks.Where(kvp => kvp.Key.x < 0).Select(kvp => kvp.Value).ToList();
    }

    public void UpdateChunks(Vector3 shipPosition)
    {
        Dictionary<Vector3Int, Chunk> currentChunks = Chunks;
        Dictionary<Vector3Int, Chunk> newChunks = new Dictionary<Vector3Int, Chunk>();
        Chunk centerChunk = currentChunks[Vector3Int.zero];
        Vector3 centerChunkCenterPosition = centerChunk.transform.position;
        float xDelta = shipPosition.x - centerChunkCenterPosition.x;
        float yDelta = shipPosition.y - centerChunkCenterPosition.y;
        float zDelta = shipPosition.z - centerChunkCenterPosition.z;

        //ship moved to the right
        if (shipPosition.x > centerChunk.transform.position.x + ChunkDimensions.x / 2)
        {
            foreach(KeyValuePair<Vector3Int , Chunk> kvp in currentChunks)
            {
                if(kvp.Key.x < 0)
                {
                    Destroy(kvp.Value.gameObject);
                }
                else
                {
                    newChunks.Add(kvp.Key + Vector3Int.left, kvp.Value);
                }
            }

            for(int y = -1; y <=1; y++)
            {
                for(int z = -1; z <=1; z++)
                {
                    Chunk newChunk = Instantiate(ChunkPrefab, transform);
                    newChunk.GenerateChunk(new Vector3(ChunkDimensions.x - 1, y * (ChunkDimensions.y - 1), z * (ChunkDimensions.z - 1)) + newChunks[Vector3Int.zero].transform.position,true);
                    newChunks.Add(new Vector3Int(1,y,z), newChunk);
                }
            }

            Chunks.Clear();
            Chunks = newChunks; 
        }

        //ship moved to the left
        if (shipPosition.x < centerChunk.transform.position.x - ChunkDimensions.x / 2)
        {
            foreach (KeyValuePair<Vector3Int, Chunk> kvp in currentChunks)
            {
                if (kvp.Key.x > 0)
                {
                    Destroy(kvp.Value.gameObject);
                }
                else
                {
                    newChunks.Add(kvp.Key + Vector3Int.right, kvp.Value);
                }
            }

            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Chunk newChunk = Instantiate(ChunkPrefab, transform);
                    newChunk.GenerateChunk(new Vector3(-1 * ChunkDimensions.x - 1, y * (ChunkDimensions.y - 1), z * (ChunkDimensions.z - 1)) + newChunks[Vector3Int.zero].transform.position, true);
                    newChunks.Add(new Vector3Int(-1, y, z), newChunk);
                }
            }

            Chunks.Clear();
            Chunks = newChunks;
        }

        //ship moved up
        if (shipPosition.y > centerChunk.transform.position.y + ChunkDimensions.y / 2)
        {
            foreach (KeyValuePair<Vector3Int, Chunk> kvp in currentChunks)
            {
                if (kvp.Key.y < 0)
                {
                    Destroy(kvp.Value.gameObject);
                }
                else
                {
                    newChunks.Add(kvp.Key + Vector3Int.down, kvp.Value);
                }
            }

            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Chunk newChunk = Instantiate(ChunkPrefab, transform);
                    newChunk.GenerateChunk(new Vector3(x * (ChunkDimensions.x - 1), 1*(ChunkDimensions.y - 1), z * (ChunkDimensions.z - 1)) + newChunks[Vector3Int.zero].transform.position, true);
                    newChunks.Add(new Vector3Int(x, 1, z), newChunk);
                }
            }

            Chunks.Clear();
            Chunks = newChunks;
        }

        //ship moved down
        if (shipPosition.y < centerChunk.transform.position.y - ChunkDimensions.y / 2)
        {
            foreach (KeyValuePair<Vector3Int, Chunk> kvp in currentChunks)
            {
                if (kvp.Key.y > 0)
                {
                    Destroy(kvp.Value.gameObject);
                }
                else
                {
                    newChunks.Add(kvp.Key + Vector3Int.up, kvp.Value);
                }
            }

            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Chunk newChunk = Instantiate(ChunkPrefab, transform);
                    newChunk.GenerateChunk(new Vector3(x * (ChunkDimensions.x - 1), -1*(ChunkDimensions.y - 1), z * (ChunkDimensions.z - 1)) + newChunks[Vector3Int.zero].transform.position, true);
                    newChunks.Add(new Vector3Int(x, -1, z), newChunk);
                }
            }

            Chunks.Clear();
            Chunks = newChunks;

        }
        //ship moved forward
        if (shipPosition.z > centerChunk.transform.position.z + ChunkDimensions.z / 2)
        {
            foreach (KeyValuePair<Vector3Int, Chunk> kvp in currentChunks)
            {
                if (kvp.Key.z < 0)
                {
                    Destroy(kvp.Value.gameObject);
                }
                else
                {
                    newChunks.Add(kvp.Key + Vector3Int.back, kvp.Value);
                }
            }

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Chunk newChunk = Instantiate(ChunkPrefab, transform);
                    newChunk.GenerateChunk(new Vector3(x * (ChunkDimensions.x - 1), y * (ChunkDimensions.y - 1), 1 * (ChunkDimensions.z - 1)) + newChunks[Vector3Int.zero].transform.position, true);
                    newChunks.Add(new Vector3Int(x, y, 1), newChunk);
                }
            }

            Chunks.Clear();
            Chunks = newChunks;
        }

        //ship moved back
        if (shipPosition.z < centerChunk.transform.position.z - ChunkDimensions.z / 2)
        {
            foreach (KeyValuePair<Vector3Int, Chunk> kvp in currentChunks)
            {
                if (kvp.Key.z > 0)
                {
                    Destroy(kvp.Value.gameObject);
                }
                else
                {
                    newChunks.Add(kvp.Key + Vector3Int.forward, kvp.Value);
                }
            }

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Chunk newChunk = Instantiate(ChunkPrefab, transform);
                    newChunk.GenerateChunk(new Vector3(x * (ChunkDimensions.x - 1), y * (ChunkDimensions.y - 1), -1 * (ChunkDimensions.z - 1)) + newChunks[Vector3Int.zero].transform.position, true);
                    newChunks.Add(new Vector3Int(x, y, -1), newChunk);
                }
            }

            Chunks.Clear();
            Chunks = newChunks;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
