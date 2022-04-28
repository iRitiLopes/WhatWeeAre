using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropable : Dropable, IDropHandler
{
    public new void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            base.OnDrop(eventData);            
            var itemSlot = eventData.pointerDrag.transform.GetChild(0).GetComponent<ItemSlot>();
            Inventory.addItem(itemSlot.item.id, itemSlot.quantity);
        }
    }
}
