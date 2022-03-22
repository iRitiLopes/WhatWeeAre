using System;
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

    bool isLoaded = false;
    private void FixedUpdate() {
        if (!disassembleSlot.GetComponent<Dropable>().canBeDroped) {
            if(disassembleSlot.transform.childCount < 1){
                CleanOutputItems();
                return;
            }
            var itemSlot = disassembleSlot.transform.GetChild(0).transform.GetChild(0).GetComponent<ItemSlot>();
            if (!itemSlot.item.IsRaw) {
                if (itemSlot.item.RawItems.Count > 3) {
                    return;
                }

                if(isLoaded){
                    return;
                }
                int slotNumber = 1;
                Debug.Log(itemSlot.item.RawItems.Count);
                foreach (RawItem rawItem in itemSlot.item.RawItems) {
                    AddOutputItem(ItemDatabase.findItem(rawItem.id), slotNumber, rawItem.quantity);
                    slotNumber++;
                }
                isLoaded = true;
            }
        } else {
            CleanOutputItems();
        }
    }

    private void CleanOutputItems() {

        foreach(Transform child in output1.transform){
            Destroy(child.gameObject);
        }

        foreach(Transform child in output2.transform){
            Destroy(child.gameObject);
        }

        foreach(Transform child in output3.transform){
            Destroy(child.gameObject);
        }
        isLoaded = false;
    }


    private void AddOutputItem(Item item, int outputSlot, int quantity) {
        GameObject wrapper;
        switch (outputSlot) {
            case 1:
                wrapper = CreateDragDropObject.createWrapper(output1.transform, canvas);
                putItem(wrapper, outputSlot, item, quantity);
                wrapper.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                break;
            case 2:
                wrapper = CreateDragDropObject.createWrapper(output2.transform, canvas);
                putItem(wrapper, outputSlot, item, quantity);
                wrapper.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                break;
            case 3:
                wrapper = CreateDragDropObject.createWrapper(output3.transform, canvas);
                putItem(wrapper, outputSlot, item, quantity);
                wrapper.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                wrapper.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                break;
            default:
                break;
        }
        
    }

    private void putItem(GameObject wrapper, int outputSlot, Item item, int quantity){
        var children = PrefabUtility.InstantiatePrefab(slot, wrapper.transform) as GameObject;
        wrapper.name = "InfinityItemSlotWrapper_" + outputSlot;
        children.transform.Find("ItemSlot").GetComponent<Image>().sprite = item.icon;
        children.GetComponent<ItemSlot>().item = item;
        var qt = children.transform.Find("Quantity");
        qt.GetComponent<TMPro.TextMeshProUGUI>().text = quantity.ToString();
    }
}   