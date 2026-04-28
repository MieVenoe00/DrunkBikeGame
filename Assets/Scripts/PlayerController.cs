using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float minY = -1.2f;
    public float maxY = 1.2f;

    void Update()
    {
        float input = 0f;

        if (Keyboard.current.upArrowKey.isPressed)
            input = 1f;
        else if (Keyboard.current.downArrowKey.isPressed)
            input = -1f;

        Vector3 pos = transform.position;
        pos.y += input * moveSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}