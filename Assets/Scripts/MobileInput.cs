using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static bool upPressed = false;
    public static bool downPressed = false;
    public static bool switchPressed = false;

    public void PressUp()    { upPressed = true; }
    public void PressDown()  { downPressed = true; }
    public void PressSwitch(){ switchPressed = true; }
}