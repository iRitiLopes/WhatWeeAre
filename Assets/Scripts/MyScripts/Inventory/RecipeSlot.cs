using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

public class RecipeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Item item;

    public PowerUpEffect powerUpEffect;

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
        string name = item != null ? item.name : LocalizationSettings.StringDatabase.GetLocalizedString("power_names", powerUpEffect.powerUpName);
        string description = item != null ? item.description : LocalizationSettings.StringDatabase.GetLocalizedString("power_descriptions", powerUpEffect.powerUpName);
        Tooltip.show(name + " - " + description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.hide();
    }
}
