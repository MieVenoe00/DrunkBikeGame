using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float[] lanes = { -0.9f, 0f, 0.9f };
    private int currentLane = 1; // Starter på midten (0)

    void Start()
    {
        // Sæt startposition
        transform.position = new Vector3(transform.position.x, lanes[currentLane], 0);
    }

    void Update()
    {
          // Bevæg ikke spilleren hvis Spawner ikke er aktiv (spillet ikke startet)
    if (!GameObject.Find("Spawner").activeInHierarchy) return;

    if (Keyboard.current.upArrowKey.wasPressedThisFrame)
    // ... resten af din kode
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (currentLane < lanes.Length - 1) // Må ikke gå over øverste bane
            {
                currentLane++;
                MoveTolane();
            }
        }

        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            if (currentLane > 0) // Må ikke gå under nederste bane
            {
                currentLane--;
                MoveTolane();
            }
        }
    }

    void MoveTolane()
    {
        transform.position = new Vector3(transform.position.x, lanes[currentLane], 0);
    }
}