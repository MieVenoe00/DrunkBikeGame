using UnityEngine;
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
    public GameObject gameObjectToEnable;
    public GameObject gameObjectToDisable;

    [Header("Animationer der starter ved play")]
    public Animator[] animatorsToEnable;

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;

        // Hvis det er replay — spring start skærm over
        if (GameState.isReplay)
        {
            GameState.isReplay = false;

            if (gameObjectToEnable != null)
                gameObjectToEnable.SetActive(true);

            if (gameObjectToDisable != null)
                gameObjectToDisable.SetActive(false);

            foreach (Animator anim in animatorsToEnable)
                anim.enabled = true;

            gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) { buttonImage.sprite = hoverSprite; }
    public void OnPointerExit(PointerEventData eventData) { buttonImage.sprite = normalSprite; }
    public void OnPointerDown(PointerEventData eventData) { buttonImage.sprite = pressedSprite; }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = releasedSprite;

        if (gameObjectToEnable != null)
            gameObjectToEnable.SetActive(true);

        if (gameObjectToDisable != null)
            gameObjectToDisable.SetActive(false);

        foreach (Animator anim in animatorsToEnable)
            anim.enabled = true;

        gameObject.SetActive(false);
    }
}