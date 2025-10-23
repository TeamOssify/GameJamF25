using UnityEngine;
using UnityEngine.EventSystems;

public class MenuHoverEnter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private SlidingMenu slidingMenu;

    public void OnPointerEnter(PointerEventData eventData) {
        slidingMenu.ShowMenu();
    }
}
