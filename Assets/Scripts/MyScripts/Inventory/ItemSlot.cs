using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Item item;

    public int quantity = 0;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        var qt = transform.Find("Quantity");
        qt.GetComponent<TMPro.TextMeshProUGUI>().text = quantity.ToString();
    }


    public void OnPointerEnter(PointerEventData eventData) {
        string message = item.name + " - " + item.description;
        if (!item.IsRaw) {
            message = message + "\n" + "Disassemble in: ";
            foreach (RawItem rawItem in item.RawItems) {
                Item item = ItemDatabase.findItem(rawItem.id);
                message = message + "\n - " + item.name + ", quantity: " + rawItem.quantity;
            }
        }
        Tooltip.show(message);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.hide();
    }
}
