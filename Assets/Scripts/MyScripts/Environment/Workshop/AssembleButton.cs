using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

public class AssembleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {

    }

    public void OnPointerEnter(PointerEventData eventData) {
        Tooltip.show(LocalizationSettings.StringDatabase.GetLocalizedString("tooltip", "assemble"));
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.hide();
    }

    public void OnPointerClick(PointerEventData eventData) {
        Assembler.assemble();
    }
}
