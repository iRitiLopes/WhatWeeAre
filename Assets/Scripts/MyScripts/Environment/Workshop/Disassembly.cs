using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Disassembly : MonoBehaviour {
    [SerializeField]
    GameObject disassembleSlot;

    [SerializeField]
    GameObject output1;

    [SerializeField]
    GameObject output2;

    [SerializeField]
    GameObject output3;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject slot;

    private static Disassembly instance;

    ItemSlot actualItem = null;

    List<RawItem> outputItems;

    private void Awake() {
        instance = this;
        outputItems = new List<RawItem>();
    }

    bool isLoaded = false;

    private void FixedUpdate() {
        if (!disassembleSlot.GetComponent<Dropable>().canBeDroped) {
            if (
                !ChildFinder
                    .existChildWithName(disassembleSlot.transform,
                    "InfinityItemSlotWrapper")
            ) {
                CleanOutputItems();
                return;
            }
            var itemSlot =
                ChildFinder
                    .findWithStartName(disassembleSlot.transform,
                    "InfinityItemSlotWrapper")
                    .GetChild(0)
                    .GetComponent<ItemSlot>();

            actualItem = itemSlot;
            if (!itemSlot.item.IsRaw) {
                if (isLoaded) {
                    return;
                }
                Inventory.notShowItem(actualItem.item.id);
                int slotNumber = 1;
                foreach (RawItem rawItem in itemSlot.item.RawItems) {
                    outputItems.Add(rawItem);
                    AddOutputItem(ItemDatabase.findItem(rawItem.id),
                    slotNumber,
                    rawItem.quantity);
                    slotNumber++;
                }
                isLoaded = true;
            }
        } else {
            CleanOutputItems();
        }
    }

    private void CleanDisassemble() {
        if (isLoaded || actualItem != null) {
            var itemSlot =
                ChildFinder
                    .findWithStartName(disassembleSlot.transform,
                    "InfinityItemSlotWrapper");
            Destroy(itemSlot.gameObject);
            actualItem = null;
            CleanOutputItems();
        }
    }

    private void CleanOutputItems() {
        foreach (Transform child in output1.transform) {
            Destroy(child.gameObject);
        }

        foreach (Transform child in output2.transform) {
            Destroy(child.gameObject);
        }

        foreach (Transform child in output3.transform) {
            Destroy(child.gameObject);
        }
        outputItems = new List<RawItem>();
        isLoaded = false;
    }

    private void AddOutputItem(Item item, int outputSlot, int quantity) {
        GameObject wrapper;
        switch (outputSlot) {
            case 1:
                wrapper =
                    CreateDragDropObject
                        .createWrapper(output1.transform, canvas, false);
                putItem(wrapper, outputSlot, item, quantity);
                wrapper.GetComponent<RectTransform>().anchorMax =
                    new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchorMin =
                    new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchoredPosition3D =
                    Vector3.zero;
                break;
            case 2:
                wrapper =
                    CreateDragDropObject
                        .createWrapper(output2.transform, canvas, false);
                putItem(wrapper, outputSlot, item, quantity);
                wrapper.GetComponent<RectTransform>().anchorMax =
                    new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchorMin =
                    new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchoredPosition3D =
                    Vector3.zero;
                break;
            case 3:
                wrapper =
                    CreateDragDropObject
                        .createWrapper(output3.transform, canvas, false);
                putItem(wrapper, outputSlot, item, quantity);
                wrapper.GetComponent<RectTransform>().anchorMax =
                    new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchorMin =
                    new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchoredPosition3D =
                    Vector3.zero;
                break;
            default:
                break;
        }
    }

    private void putItem(
        GameObject wrapper,
        int outputSlot,
        Item item,
        int quantity
    ) {
        var children =
            UnityEngine.Object.Instantiate(slot, wrapper.transform) as
            GameObject;
        wrapper.name = "InfinityItemSlotWrapper_" + outputSlot;
        children.transform.Find("ItemSlot").GetComponent<Image>().sprite =
            item.icon;
        children.GetComponent<ItemSlot>().item = item;
        children.GetComponent<ItemSlot>().quantity = quantity;
    }

    public static void disassemble() {
        if (instance.outputItems.Count < 1 || instance.actualItem == null) {
            Debug.Log("This item doesnt provide");
            return;
        }

        if (instance.actualItem.quantity == 1) {
            instance._disassemble();
            instance.CleanDisassemble();
            return;
        }

        instance._disassemble();
    }

    private void _disassemble() {
        Inventory.removeItem(actualItem.item.id, 1);
        actualItem.quantity = actualItem.quantity - 1;
        var itemSlot =
            ChildFinder
                .findWithStartName(disassembleSlot.transform,
                "InfinityItemSlotWrapper")
                .GetChild(0)
                .GetComponent<ItemSlot>();
        itemSlot.quantity = actualItem.quantity;

        foreach (var item in outputItems) {
            Inventory.AddItem(item.id, item.quantity);
        }
    }
}
