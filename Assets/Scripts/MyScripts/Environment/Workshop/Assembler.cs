using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Assembler : MonoBehaviour, Notificable {
    [SerializeField]
    GameObject outputSlot;

    [SerializeField]
    GameObject input1;

    [SerializeField]
    GameObject input2;

    [SerializeField]
    GameObject input3;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject slot;

    private static Assembler instance;

    ItemSlot actualItem = null;

    List<ItemSlot> inputItens = new List<ItemSlot>();

    private void Awake() {
        instance = this;
        input1.GetComponent<Dropable>().subscribe(this);
        input2.GetComponent<Dropable>().subscribe(this);
        input3.GetComponent<Dropable>().subscribe(this);
    }

    public void notify(GameObject go) {
        Debug.Log("Notified");
        inputItens.Add(go.transform.GetChild(0).GetComponent<ItemSlot>());
        var found = ItemDatabase.findItemByComponents(inputItens);
        if (found != null) {
            AddOutputItem(found);
            Debug.Log("Achei");
            Debug.Log(found.name);
        }
    }

    private void AddOutputItem(Item item) {
        GameObject wrapper;
        wrapper = CreateDragDropObject.createWrapper(outputSlot.transform, canvas, false);
        putItem(wrapper, item, 1);
        wrapper.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        wrapper.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        wrapper.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    }

    private void putItem(GameObject wrapper, Item item, int quantity) {
        var children = UnityEngine.Object.Instantiate(slot, wrapper.transform) as GameObject;
        wrapper.name = "InfinityItemSlotWrapper_" + outputSlot;
        children.transform.Find("ItemSlot").GetComponent<Image>().sprite = item.icon;
        children.GetComponent<ItemSlot>().item = item;
        children.GetComponent<ItemSlot>().quantity = quantity;
        actualItem = children.GetComponent<ItemSlot>();
    }

    public static void assemble() {
        if (instance.actualItem == null) {
            Debug.Log("This item doesnt provide");
            return;
        }

        instance._assemble();
        instance.CleanDisassemble();
    }

    private void _assemble() {
        Inventory.AddItem(actualItem.item.id, 1);
        foreach (var inputItem in inputItens) {
            Inventory.removeItem(inputItem.item.id, actualItem.item.RawItems.Find(x => x.id.Equals(inputItem.item.id)).quantity);
            inputItem.quantity = actualItem.item.RawItems.Find(x => x.id.Equals(inputItem.item.id)).quantity;
        }
    }

    private void CleanDisassemble() {
        if (actualItem != null) {
            var itemSlot = ChildFinder.findWithStartName(outputSlot.transform, "InfinityItemSlotWrapper");
            Destroy(itemSlot.gameObject);
            actualItem = null;
            CleanInputItems();
        }
    }

    private void CleanInputItems() {
        foreach (Transform child in input1.transform) {
            Destroy(child.gameObject);
        }

        foreach (Transform child in input2.transform) {
            Destroy(child.gameObject);
        }

        foreach (Transform child in input3.transform) {
            Destroy(child.gameObject);
        }
        inputItens = new List<ItemSlot>();
    }

}
