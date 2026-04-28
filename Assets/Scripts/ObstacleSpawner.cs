using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float spawnX = 4f;
    public float minY = -1.2f;
    public float maxY = 1.2f;

    void Start()
    {
        InvokeRepeating("Spawn", 1f, spawnInterval);
    }

    void Spawn()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(spawnX, randomY, 0);
        Instantiate(obstaclePrefab, pos, Quaternion.identity);
    }
}