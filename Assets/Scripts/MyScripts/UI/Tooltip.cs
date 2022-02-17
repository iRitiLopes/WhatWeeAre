using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    private TMPro.TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;

    [SerializeField]
    private Camera uiCamera;

    private static Tooltip instance;

    private void Awake() {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
        instance = this;
        HideTooltip();
    }

    private void Update() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;

    }

    private void ShowTooltip(string tooltipString) {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 bgSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = bgSize;
    }

    private void HideTooltip() {
        gameObject.SetActive(false);
    }

    public static void hide(){
        instance.HideTooltip();
    }

    public static void show(string txt){
        instance.ShowTooltip(txt);
    }
}
