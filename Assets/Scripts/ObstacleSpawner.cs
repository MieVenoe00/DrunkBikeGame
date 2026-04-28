using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Normal World - Biler")]
    public GameObject[] carPrefabs;        // Træk dine 3 bil-prefabs ind her

    [Header("Magic World - Ildkugler")]
    public GameObject[] fireballPrefabs;   // Træk dine 3 ildkugle-prefabs ind her

    [Header("Spawner indstillinger")]
    public float spawnInterval = 2f;
    public float spawnX = 4f;
    public float minY = -1.2f;
    public float maxY = 1.2f;

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

        // Vælg tilfældig bil eller ildkugle
        int randomIndex = Random.Range(0, currentPrefabs.Length);
        GameObject prefabToSpawn = currentPrefabs[randomIndex];

        float randomY = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(spawnX, randomY, 0);
        Instantiate(prefabToSpawn, pos, Quaternion.identity);
    }
}