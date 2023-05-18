using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Dropable : MonoBehaviour, IDropHandler {

    [SerializeField]
    readonly GameObject dropInside;

    [SerializeField]
    public bool canBeDroped = true;
    readonly List<Notificable> notificables = new();
    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null) {
            if (!canBeDroped) {
                eventData.pointerDrag.GetComponent<DragDrop>().revertParent();
                return;
            }
            if(eventData.pointerDrag.GetComponent<DragDrop>() == null){
                return;
            }
            eventData.pointerDrag.transform.SetParent(gameObject.transform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            canBeDroped = !canBeDroped;
            notifyAll(eventData.pointerDrag);
        }
    }

    private void notifyAll(GameObject go) {
        foreach (var item in notificables) {
            item.notify(go);
        }
    }

    private void FixedUpdate() {
        if (!ChildFinder.existChildWithName(transform, "InfinityItemSlotWrapper")) {
            canBeDroped = true;
        }
    }

    public void subscribe(Notificable notificable) {
        notificables.Add(notificable);
    }
}