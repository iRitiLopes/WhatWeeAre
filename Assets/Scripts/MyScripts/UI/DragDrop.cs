using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;
    private RectTransform rectTransform;

    private CanvasGroup canvasGroup;

    Vector2 originalPos;

    private Transform lastParent;

    public Canvas Canvas { get => canvas; set => canvas = value; }

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        lastParent = transform.parent;
    }

    public void changeParent(Transform newParent){
        lastParent = newParent;
    }

    public void revertParent(){
        transform.parent = lastParent;
    }
    public void OnBeginDrag(PointerEventData eventData) {
        transform.parent = canvas.transform;
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if(transform.parent.gameObject.name == "Canvas"){
            revertParent();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
