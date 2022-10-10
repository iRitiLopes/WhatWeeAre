using UnityEngine;

public class Tooltip : MonoBehaviour {

    private TMPro.TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTransform;

    [SerializeField]
    private Camera uiCamera;

    private float minWidth = 197;
    private float minHeight = 121;

    private static Tooltip instance;

    private void Awake() {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = backgroundRectTransform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
        instance = this;
        HideTooltip();
    }

    private void FixedUpdate() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;

    }

    private void ShowTooltip(string tooltipString) {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 bgSize;
        bgSize = new Vector2(minWidth + textPaddingSize, minHeight + textPaddingSize);
        tooltipText.transform.localPosition = new Vector3(bgSize.x / 2, bgSize.y / 2, 0);

        backgroundRectTransform.sizeDelta = bgSize;
    }

    private void HideTooltip() {
        gameObject.SetActive(false);
    }

    public static void hide() {
        instance.HideTooltip();
    }

    public static void show(string txt) {
        instance.ShowTooltip(txt);
    }
}
