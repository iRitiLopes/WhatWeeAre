using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {
    public GameObject slot;

    [SerializeField]
    private Canvas canvas;

    private IEnumerator Start() {
        yield return new WaitUntil(() => Inventory.instance != null);
    }

    public void loadItems(List<PlayerItem> items) {
        int i = 0;
        foreach (var item in items) {
            if (!item.show) {
                continue;
            }
            var wrapper = CreateDragDropObject.createWrapper(transform, canvas);

            var children = UnityEngine.Object.Instantiate(slot, wrapper.transform) as GameObject;

            wrapper.name = "InfinityItemSlotWrapper_" + i++;
            children.transform.Find("ItemSlot").GetComponent<Image>().sprite = item.Item.icon;
            children.GetComponent<ItemSlot>().item = item.Item;
            children.GetComponent<ItemSlot>().quantity = item.Quantity;

            var qt = children.transform.Find("Quantity");
            qt.GetComponent<TMPro.TextMeshProUGUI>().text = item.Quantity.ToString();
        }

    }
}