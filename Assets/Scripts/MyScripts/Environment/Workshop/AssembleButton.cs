using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Tooltip.show("Assemble!");
    }

    public void OnPointerExit(PointerEventData eventData) {
        Tooltip.hide();
    }

    public void OnPointerClick(PointerEventData eventData) {
        Assembler.assemble();
    }
}