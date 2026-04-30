using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Sprites")]
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite pressedSprite;
    public Sprite releasedSprite;

    [Header("Hvad sker der når man klikker?")]
    public GameObject gameObjectToEnable;  // Fx dit spil / GameManager
    public GameObject gameObjectToDisable; // Fx din startmenu

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;
    }

    // Musen hover hen over knappen
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite;
    }

    // Musen forlader knappen
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite;
    }

    // Musen trykker ned
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = pressedSprite;
    }

    // Musen slipper
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = releasedSprite;

        // Start spillet
        if (gameObjectToEnable != null)
            gameObjectToEnable.SetActive(true);

        if (gameObjectToDisable != null)
            gameObjectToDisable.SetActive(false);
    }
}