using System;
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

    public void loadItems(Dictionary<string, PlayerItem> items) {
        int i = 0;
        foreach (var item in items.Keys) {
            if (!items[item].show) {
                continue;
            }
            var wrapper = CreateDragDropObject.createWrapper(transform, canvas);

            var children = Instantiate(slot, wrapper.transform) as GameObject;

            wrapper.name = "InfinityItemSlotWrapper_" + i++;
            children.transform.Find("ItemSlot").GetComponent<Image>().sprite = items[item].Item.icon;
            children.GetComponent<ItemSlot>().item = items[item].Item;
            children.GetComponent<ItemSlot>().quantity = items[item].Quantity;

            var qt = children.transform.Find("Quantity");
            qt.GetComponent<TMPro.TextMeshProUGUI>().text = items[item].Quantity.ToString();
        }

    }
}