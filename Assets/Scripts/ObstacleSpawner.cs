using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Normal World - Biler")]
    public GameObject[] carPrefabs;

    [Header("Magic World - Ildkugler")]
    public GameObject[] fireballPrefabs;

    [Header("Spawn Interval per Level")]
    public float spawnIntervalLevel1 = 2f;
    public float spawnIntervalLevel2 = 2f;
    public float spawnX = 4f;

    [Header("Level System")]
    public float levelDuration = 30f;

    [Header("Level 1 Sekvenser")]
    public int[] normalSequenceLevel1 = { 1, 1, 1, 2, 1, 2, 2, 1 };
    public int[] magicSequenceLevel1  = { 1, 1, 2, 1, 1, 2, 2, 1 };

    [Header("Level 2 Sekvenser")]
    public int[] normalSequenceLevel2 = { 2, 2, 2, 3, 2, 3, 2, 3 };
    public int[] magicSequenceLevel2  = { 2, 2, 3, 2, 3, 2, 3, 3 };

    [Header("Hastighed")]
    public float baseSpeed = 3f;
    public float maxSpeed = 6f;
    public float totalSpeedDuration = 60f;

    private float[] lanes = { 0.9f, 0f, -0.9f };
    private int currentLevel = 0;
    private float currentSpeed;
    private float levelTimer = 0f;
    private float globalTimer = 0f;

    private int normalSequenceIndex = 0;
    private int magicSequenceIndex = 0;

    private Coroutine normalCoroutine;
    private Coroutine magicCoroutine;

    void Start()
    {
        currentSpeed = baseSpeed;
        normalCoroutine = StartCoroutine(SpawnNormalWorld(1f));
        magicCoroutine = StartCoroutine(SpawnMagicWorld(1f));
        StartCoroutine(LevelTimer());
    }

    void Update()
    {
        globalTimer += Time.deltaTime;
        levelTimer += Time.deltaTime;
        float t = Mathf.Clamp01(globalTimer / totalSpeedDuration);
        currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, t);
    }

    // ← Nu en selvstændig metode og ikke inde i Update!
    float GetCurrentInterval()
    {
        return currentLevel == 0 ? spawnIntervalLevel1 : spawnIntervalLevel2;
    }

    public void ResetSpawnTimer(bool switchedToNormal)
    {
        if (switchedToNormal)
        {
            StopCoroutine(normalCoroutine);
            normalCoroutine = StartCoroutine(SpawnNormalWorld(GetCurrentInterval()));
        }
        else
        {
            StopCoroutine(magicCoroutine);
            magicCoroutine = StartCoroutine(SpawnMagicWorld(GetCurrentInterval()));
        }
    }

    IEnumerator LevelTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(levelDuration);
            currentLevel++;
            normalSequenceIndex = 0;
            magicSequenceIndex = 0;
            levelTimer = 0f;
            Debug.Log("Level " + (currentLevel + 1) + " started!");
        }
    }

    IEnumerator SpawnNormalWorld(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            int[] sequence = currentLevel == 0 ? normalSequenceLevel1 : normalSequenceLevel2;
            int count = sequence[normalSequenceIndex % sequence.Length];
            SpawnObstacles(carPrefabs, count, isNormalWorld: true);
            normalSequenceIndex++;
            yield return new WaitForSeconds(GetCurrentInterval());
        }
    }

    IEnumerator SpawnMagicWorld(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            int[] sequence = currentLevel == 0 ? magicSequenceLevel1 : magicSequenceLevel2;
            int count = sequence[magicSequenceIndex % sequence.Length];
            SpawnObstacles(fireballPrefabs, count, isNormalWorld: false);
            magicSequenceIndex++;
            yield return new WaitForSeconds(GetCurrentInterval());
        }
    }

    void SpawnObstacles(GameObject[] prefabs, int count, bool isNormalWorld)
    {
        int[] shuffledLanes = ShuffleLanes();

        for (int i = 0; i < count; i++)
        {
            int randomPrefab = Random.Range(0, prefabs.Length);
            Vector3 pos = new Vector3(spawnX, lanes[shuffledLanes[i]], 0);
            GameObject obj = Instantiate(prefabs[randomPrefab], pos, Quaternion.identity);

            Obstacle obstacle = obj.GetComponent<Obstacle>();
            obstacle.speed = currentSpeed;
            obstacle.isNormalWorldObstacle = isNormalWorld;
        }
    }

    int[] ShuffleLanes()
    {
        int[] allLanes = { 0, 1, 2 };
        for (int i = allLanes.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = allLanes[i];
            allLanes[i] = allLanes[j];
            allLanes[j] = temp;
        }
        return allLanes;
    }
}