using UnityEngine;
using UnityEngine.EventSystems;
public class Dropable : MonoBehaviour, IDropHandler {

    [SerializeField]
    GameObject dropInside;

    [SerializeField]
    public bool canBeDroped = true;
    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null) {
            if(!canBeDroped){
                eventData.pointerDrag.GetComponent<DragDrop>().revertParent();
                return;
            }
            eventData.pointerDrag.transform.SetParent(dropInside.transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            canBeDroped = !canBeDroped;
        }
    }

    private void FixedUpdate() {
        if(!ChildFinder.existChildWithName(transform, "InfinityItemSlotWrapper")){
            canBeDroped = true;
        }
    }
}