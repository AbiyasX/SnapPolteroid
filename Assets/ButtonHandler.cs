using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UnityEvent EnterHover;
    [SerializeField] UnityEvent ExitHover;

    public Image buttonImage; 
    public Sprite hoverSprite; 
    private Sprite defaultSprite;

    private void Awake()
    {
        defaultSprite = buttonImage.sprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite;
        EnterHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite;
        ExitHover.Invoke();
    }

}
