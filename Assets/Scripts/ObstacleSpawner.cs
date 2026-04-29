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
            // Alle mulige par af baner
                int[][] lanePairs = new int[][]
                {
                    new int[] { 0, 1 },  // 0.9 og 0
                    new int[] { 0, 2 },  // 0.9 og -0.9
                    new int[] { 1, 2 },  // 0 og -0.9
                };

                // Vælg et tilfældigt par
                int[] chosenPair = lanePairs[Random.Range(0, lanePairs.Length)];

                SpawnInLane(currentPrefabs, chosenPair[0]);
                SpawnInLane(currentPrefabs, chosenPair[1]);
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