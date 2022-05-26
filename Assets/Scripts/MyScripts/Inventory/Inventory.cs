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
    public List<PlayerItem> items = new List<PlayerItem>();
    public Dictionary<Guid, PlayerItem> playerItems = new Dictionary<Guid, PlayerItem>();

    public static Inventory instance;

    InventoryDisplay inventoryDisplay = null;

    [SerializeField]
    String INVENTORY_PATH = "./Assets/Sprites/items/player_items.json";

    private void Awake() {
        instance = this;
    }

    private void Start() {
        //DontDestroyOnLoad(this.gameObject);
        buildInventory();
        inventoryDisplay = FindObjectOfType<InventoryDisplay>(true);
        if(inventoryDisplay != null){
            inventoryDisplay.loadItems(this.items);
        }
    }

    public void buildInventory() {

        JArray data = JArray.Parse(File.ReadAllText(INVENTORY_PATH));
        foreach (JObject item in data) {
            Guid id = Guid.Parse((string?)item.GetValue("id"));
            int quantity = ((int)item.GetValue("quantity"));
            Item it = ItemDatabase.findItem(id);

            if (it != null)
                items.Add(new PlayerItem(it, quantity));
                playerItems[it.id] = new PlayerItem(it, quantity);
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
        if(inventoryDisplay != null){
            inventoryDisplay.loadItems(this.items);
        }
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

