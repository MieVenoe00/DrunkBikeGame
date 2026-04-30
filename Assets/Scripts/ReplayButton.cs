using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Sprites")]
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public Sprite pressedSprite;
    public Sprite releasedSprite;

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData) { buttonImage.sprite = hoverSprite; }
    public void OnPointerExit(PointerEventData eventData) { buttonImage.sprite = normalSprite; }
    public void OnPointerDown(PointerEventData eventData) { buttonImage.sprite = pressedSprite; }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = releasedSprite;
        GameState.isReplay = true; // ← fortæl spillet det er replay
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}