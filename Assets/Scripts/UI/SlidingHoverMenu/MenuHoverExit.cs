using System;

using UnityEngine;
using UnityEngine.EventSystems;

public class MenuHoverExit : MonoBehaviour, IPointerExitHandler
{
    [SerializeField] private SlidingMenu slidingMenu;

    public void OnPointerExit(PointerEventData eventData) {
        slidingMenu.HideMenu();
    }
}
