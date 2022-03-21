using UnityEngine;
using UnityEngine.EventSystems;
public class Dropable : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null){
            eventData.pointerDrag.transform.parent = transform;
        }
    }
}