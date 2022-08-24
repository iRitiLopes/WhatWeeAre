using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string name;
    public string description;
    public int quantity;
    public void OnPointerEnter(PointerEventData eventData) {
        string message = name + " - " + description + " - " + quantity;
        Tooltip.show(message);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.hide();
    }
}