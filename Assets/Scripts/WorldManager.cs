using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : MonoBehaviour
{
    public GameObject normalWorld;
    public GameObject normalWorld2;
    public GameObject magicWorld;
    public GameObject magicWorld2;

    private bool isNormalWorld = true;
    private ObstacleSpawner obstacleSpawner;

    [Header("Spawn Interval per Level")]
    public float spawnIntervalLevel1 = 2f;
    public float spawnIntervalLevel2 = 1.5f;

    void Start()
    {
        normalWorld.SetActive(true);
        normalWorld2.SetActive(true);
        magicWorld.SetActive(false);
        magicWorld2.SetActive(false);
        obstacleSpawner = FindAnyObjectByType<ObstacleSpawner>();
    }

    void Update()
    {
        bool switchWorld = Keyboard.current.tabKey.wasPressedThisFrame || MobileInput.switchPressed;
        MobileInput.switchPressed = false;

        if (switchWorld)
            SwitchWorld();
    }

    void SwitchWorld()
    {
        isNormalWorld = !isNormalWorld;
        normalWorld.SetActive(isNormalWorld);
        normalWorld2.SetActive(isNormalWorld);
        magicWorld.SetActive(!isNormalWorld);
        magicWorld2.SetActive(!isNormalWorld);
        obstacleSpawner.ResetSpawnTimer(isNormalWorld);
    }

    public bool IsNormalWorld() => isNormalWorld;
}