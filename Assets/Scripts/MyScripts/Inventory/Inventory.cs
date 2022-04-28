using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEditor;

public class Inventory : MonoBehaviour {
    [SerializeField]
    List<PlayerItem> items = new List<PlayerItem>();

    public GameObject slot;

    [SerializeField]
    private Canvas canvas;

    public static Inventory instance;

    [SerializeField]
    String INVENTORY_PATH = "./Assets/Sprites/items/player_items.json";

    private void Awake() {
        instance = this;
    }

    private void Start() {
        buildInventory();
        loadItems();
    }

    public void buildInventory() {

        JArray data = JArray.Parse(File.ReadAllText(INVENTORY_PATH));
        foreach (JObject item in data) {
            Guid id = Guid.Parse((string?)item.GetValue("id"));
            int quantity = ((int)item.GetValue("quantity"));
            Item it = ItemDatabase.findItem(id);

            if (it != null)
                items.Add(new PlayerItem(it, quantity));
        }
    }

    public void loadItems() {
        int i = 0;
        foreach (var item in this.items) {
            var wrapper = CreateDragDropObject.createWrapper(transform, canvas);

            var children = PrefabUtility.InstantiatePrefab(slot, wrapper.transform) as GameObject;

            wrapper.name = "InfinityItemSlotWrapper_" + i++;
            children.transform.Find("ItemSlot").GetComponent<Image>().sprite = item.Item.icon;
            children.GetComponent<ItemSlot>().item = item.Item;
            children.GetComponent<ItemSlot>().quantity = item.Quantity;

            var qt = children.transform.Find("Quantity");
            qt.GetComponent<TMPro.TextMeshProUGUI>().text = item.Quantity.ToString();
        }

    }

    public void clearInventory(){
        foreach(Transform child in transform){
            GameObject.Destroy(child.gameObject);
        }
    }

    private void refresh(){
        clearInventory();
        storeItems();
        loadItems();
    }

    private void storeItems() {
        List<String> jsonElements = new List<String>();
        foreach(PlayerItem playerItem in items){
            jsonElements.Add(playerItem.ToJson());
        }
        String json = String.Join(",", jsonElements);
        json = "[" + json + "]";
        File.WriteAllText(INVENTORY_PATH, json);
    }

    private void _removeItem(Guid id, int quantity){
        var idx = items.FindIndex(x => x.Item.id.Equals(id));
        var item = items[idx];
        if(quantity < item.Quantity){
            item.Quantity = item.Quantity - quantity;
            items[idx] = item;
        }else if(quantity == item.Quantity){
            items.RemoveAt(idx);
        }else{
            Debug.Log("Cannot consume item!");
        }
        refresh();
    }

    private void _removeItem(Guid id){
        var idx = items.FindIndex(x => x.Item.id.Equals(id));
        if(idx == -1){
            return;
        }
        items.RemoveAt(idx);
        refresh();
    }

    private void _addItem(Guid id, int quantity){
        var idx = items.FindIndex(x => x.Item.id.Equals(id));
        if(idx == -1){
            Item it = ItemDatabase.findItem(id);
            items.Add(new PlayerItem(it, quantity));
            refresh();
            return;
        }
        var item = items[idx];
        item.Quantity = item.Quantity + quantity;
        items[idx] = item;
        refresh();
    }

    public static void removeItem(Guid id, int quantity){
        instance._removeItem(id, quantity);
    }

    public static void removeItem(Guid id){
        instance._removeItem(id);
    }

    public static void addItem(Guid id, int quantity){
        instance._addItem(id, quantity);
    }
}

