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

    [SerializeField]
    Message messageManager;

    private static Assembler instance;

    ItemSlot actualItem = null;

    Dictionary<string, ItemSlot> inputItens = new();

    private void Awake() {
        instance = this;
        input1.GetComponent<Dropable>().subscribe(this);
        input2.GetComponent<Dropable>().subscribe(this);
        input3.GetComponent<Dropable>().subscribe(this);
    }

    public void notify(GameObject go) {
        Debug.Log("Notified");

        
        var i1 = input1.transform.childCount > 0 ? input1.transform.GetChild(0).GetChild(0)?.GetComponent<ItemSlot>() : null;
        var n1 = input1.name;
        
        var i2 = input2.transform.childCount > 0 ? input2.transform.GetChild(0).GetChild(0)?.GetComponent<ItemSlot>() : null;
        var n2 = input2.name;

        var i3 = input3.transform.childCount > 0 ? input3.transform.GetChild(0).GetChild(0)?.GetComponent<ItemSlot>() : null;
        var n3 = input3.name;

        AddInputItem(i1, n1);
        AddInputItem(i2, n2);
        AddInputItem(i3, n3);

        Debug.Log(inputItens.Keys.Select(x => x).Aggregate((x, y) => x + " " + y));

        var found = ItemDatabase.findItemByComponents(inputItens.Values.ToList());
        Debug.Log(found);
        if (found != null) {
            AddOutputItem(found);
        } else {
            CleanDisassemble();
        }
    }

    private void AddInputItem(ItemSlot item, string name){
        if(item == null){
            inputItens.Remove(name);
            return;
        }
        inputItens.Remove(name);
        inputItens.Add(name, item);
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
        string message = "";
        Inventory.AddItem(actualItem.item.id, 1);
        foreach (var inputItem in inputItens.Values) {
            message += $" - {inputItem.item.name} x{actualItem.item.RawItems.Find(x => x.id.Equals(inputItem.item.id)).quantity}\n";
            Inventory.removeItem(inputItem.item.id, actualItem.item.RawItems.Find(x => x.id.Equals(inputItem.item.id)).quantity);
            inputItem.quantity = actualItem.item.RawItems.Find(x => x.id.Equals(inputItem.item.id)).quantity;
        }
        message += $"---------------------------- \n";
        message += $" - {actualItem.item.name}";
        messageManager.ShowMessage(message);
    }

    private void CleanDisassemble() {
        if (actualItem != null) {
            var itemSlot = ChildFinder.findWithStartName(outputSlot.transform, "InfinityItemSlotWrapper");
            inputItens = new();
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
        inputItens = new();
    }

}
