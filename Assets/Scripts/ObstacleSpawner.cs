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

    // De 3 faste baner
    private float[] lanes = { 0.9f, 0f, -0.9f };

    private WorldManager worldManager;

    void Start()
    {
        worldManager = FindAnyObjectByType<WorldManager>();
        InvokeRepeating("Spawn", 1f, spawnInterval);
    }

    void Spawn()
    {
        GameObject[] currentPrefabs;

        if (worldManager.IsNormalWorld())
            currentPrefabs = carPrefabs;
        else
            currentPrefabs = fireballPrefabs;

        // Vælg tilfældig prefab
        int randomPrefab = Random.Range(0, currentPrefabs.Length);

        // Vælg tilfældig bane
        int randomLane = Random.Range(0, lanes.Length);
        float laneY = lanes[randomLane];

        Vector3 pos = new Vector3(spawnX, laneY, 0);
        Instantiate(currentPrefabs[randomPrefab], pos, Quaternion.identity);
    }
}