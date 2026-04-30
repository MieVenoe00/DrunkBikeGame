using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float[] lanes = { -0.9f, 0f, 0.9f };
    private int currentLane = 1;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, lanes[currentLane], 0);
    }

    void Update()
    {
        if (!GameObject.Find("Spawner").activeInHierarchy) return;

        bool goUp   = Keyboard.current.upArrowKey.wasPressedThisFrame   || MobileInput.upPressed;
        bool goDown = Keyboard.current.downArrowKey.wasPressedThisFrame  || MobileInput.downPressed;

        // Reset mobile flags
        MobileInput.upPressed   = false;
        MobileInput.downPressed = false;

        if (goUp && currentLane < lanes.Length - 1)
        {
            currentLane++;
            MoveToLane();
        }

        if (goDown && currentLane > 0)
        {
            currentLane--;
            MoveToLane();
        }
    }

    void MoveToLane()
    {
        transform.position = new Vector3(transform.position.x, lanes[currentLane], 0);
    }
}