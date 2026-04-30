using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : MonoBehaviour
{
    public GameObject normalWorld;
    public GameObject magicWorld;

    private bool isNormalWorld = true;
    private ObstacleSpawner obstacleSpawner;

    [Header("Spawn Interval per Level")]
public float spawnIntervalLevel1 = 2f;
public float spawnIntervalLevel2 = 1.5f;

    void Start()
    {
        normalWorld.SetActive(true);
        magicWorld.SetActive(false);
        obstacleSpawner = FindAnyObjectByType<ObstacleSpawner>();
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
            SwitchWorld();
    }

    void SwitchWorld()
    {
        isNormalWorld = !isNormalWorld;
        normalWorld.SetActive(isNormalWorld);
        magicWorld.SetActive(!isNormalWorld);
        obstacleSpawner.ResetSpawnTimer(isNormalWorld); // ← rettet fra OnWorldSwitched
    }

    public bool IsNormalWorld() => isNormalWorld;
}
