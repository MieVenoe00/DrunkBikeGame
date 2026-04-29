using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : MonoBehaviour
{
    public GameObject normalWorld;
    public GameObject magicWorld;

    private bool isNormalWorld = true;
    private ObstacleSpawner obstacleSpawner;

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

        // Fortæl spawner at verden er skiftet
        obstacleSpawner.OnWorldSwitched(isNormalWorld);
    }

    public bool IsNormalWorld() => isNormalWorld;
}