using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RecipeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
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
        Tooltip.show(item.name);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.hide();
    }
}
