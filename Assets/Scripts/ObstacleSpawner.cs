using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Normal World - Biler")]
    public GameObject[] carPrefabs;

    [Header("Magic World - Ildkugler")]
    public GameObject[] fireballPrefabs;

    [Header("Spawn Indstillinger")]
    public float spawnInterval = 2f;
    public float spawnX = 4f;
    public float gracePeriodSpawnX = 7f; // Længere ude når man skifter verden

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

    private float[] lanes = { 0.9f, 0f, -0.9f };
    private int currentLevel = 0;
    private float currentSpeed;
    private float levelTimer = 0f;

    private int normalSequenceIndex = 0;
    private int magicSequenceIndex = 0;

    // Grace period flags
    private bool normalWorldGrace = false;
    private bool magicWorldGrace = false;

    void Start()
    {
        currentSpeed = baseSpeed;
        StartCoroutine(SpawnNormalWorld());
        StartCoroutine(SpawnMagicWorld());
        StartCoroutine(LevelTimer());
    }

    void Update()
    {
        levelTimer += Time.deltaTime;
        float t = Mathf.Clamp01((levelTimer % levelDuration) / levelDuration);
        currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, t);
    }

    // Kaldes fra WorldManager når verden skifter
    public void OnWorldSwitched(bool switchedToNormal)
    {
        if (switchedToNormal)
            normalWorldGrace = true; // Næste spawn i normal verden starter længere ude
        else
            magicWorldGrace = true;  // Næste spawn i magisk verden starter længere ude
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

    IEnumerator SpawnNormalWorld()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            int[] sequence = currentLevel == 0 ? normalSequenceLevel1 : normalSequenceLevel2;
            int count = sequence[normalSequenceIndex % sequence.Length];

            // Brug grace period X hvis verden lige er skiftet til
            float currentSpawnX = normalWorldGrace ? gracePeriodSpawnX : spawnX;
            normalWorldGrace = false; // Reset efter brug

            SpawnObstacles(carPrefabs, count, isNormalWorld: true, spawnXOverride: currentSpawnX);
            normalSequenceIndex++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnMagicWorld()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            int[] sequence = currentLevel == 0 ? magicSequenceLevel1 : magicSequenceLevel2;
            int count = sequence[magicSequenceIndex % sequence.Length];

            // Brug grace period X hvis verden lige er skiftet til
            float currentSpawnX = magicWorldGrace ? gracePeriodSpawnX : spawnX;
            magicWorldGrace = false; // Reset efter brug

            SpawnObstacles(fireballPrefabs, count, isNormalWorld: false, spawnXOverride: currentSpawnX);
            magicSequenceIndex++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObstacles(GameObject[] prefabs, int count, bool isNormalWorld, float spawnXOverride)
    {
        int[] shuffledLanes = ShuffleLanes();

        for (int i = 0; i < count; i++)
        {
            int randomPrefab = Random.Range(0, prefabs.Length);
            Vector3 pos = new Vector3(spawnXOverride, lanes[shuffledLanes[i]], 0);
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