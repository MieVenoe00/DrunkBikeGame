using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Normal World - Biler")]
    public GameObject[] carPrefabs;

    [Header("Magic World - Ildkugler")]
    public GameObject[] fireballPrefabs;

    [Header("Spawner indstillinger")]
    public float spawnInterval = 2f;
    public float spawnX = 4f;

    private float[] lanes = { 0.9f, 0f, -0.9f };
    private int spawnCount = 0;
    private WorldManager worldManager;

    void Start()
    {
        worldManager = FindAnyObjectByType<WorldManager>();
        InvokeRepeating("Spawn", 1f, spawnInterval);
    }

    void Spawn()
    {
        spawnCount++;

        GameObject[] currentPrefabs = worldManager.IsNormalWorld() ? carPrefabs : fireballPrefabs;

        if (spawnCount <= 4)
        {
            // Første 4 spawns — én obstacle i tilfældig bane
            SpawnInLane(currentPrefabs, Random.Range(0, lanes.Length));
        }
        else if (spawnCount <= 8)
        {
            // Næste 4 spawns — to obstacles i to forskellige baner
            int lane1 = Random.Range(0, lanes.Length);
            int lane2;

            // Sørg for at lane2 er forskellig fra lane1
            do { lane2 = Random.Range(0, lanes.Length); }
            while (lane2 == lane1);

            SpawnInLane(currentPrefabs, lane1);
            SpawnInLane(currentPrefabs, lane2);
        }
        else
        {
            // Efter 8 spawns — alle tre baner blokeret!
            for (int i = 0; i < lanes.Length; i++)
                SpawnInLane(currentPrefabs, i);
        }
    }

    void SpawnInLane(GameObject[] prefabs, int laneIndex)
    {
        int randomPrefab = Random.Range(0, prefabs.Length);
        Vector3 pos = new Vector3(spawnX, lanes[laneIndex], 0);
        Instantiate(prefabs[randomPrefab], pos, Quaternion.identity);
    }
}