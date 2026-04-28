using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : MonoBehaviour
{
    public GameObject normalWorld;
    public GameObject magicWorld;

    private bool isNormalWorld = true;

    void Start()
    {
        normalWorld.SetActive(true);
        magicWorld.SetActive(false);
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
    }

    public bool IsNormalWorld() => isNormalWorld;  // ← denne linje manglede!
}