using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    [SerializeField]
    public Dictionary<Guid, PlayerItem> playerItems = new();
    public static Inventory instance;

    InventoryDisplay inventoryDisplay = null;

    [SerializeField]
    readonly string INVENTORY_PATH = "./Assets/Sprites/items/player_items.json";

    private void Awake() {
        instance = this;
    }


    private void Start() {
        //DontDestroyOnLoad(this.gameObject);
        buildInventory();
        inventoryDisplay = FindObjectOfType<InventoryDisplay>(true);
        if (inventoryDisplay != null) {
            inventoryDisplay.loadItems(playerItems);
        }
    }

    public void buildInventory() {

        JArray data = JArray.Parse(File.ReadAllText(INVENTORY_PATH));
        foreach (JObject item in data) {
            Guid id = Guid.Parse((string?)item.GetValue("id"));
            int quantity = ((int)item.GetValue("quantity"));
            Item it = ItemDatabase.findItem(id);
            playerItems[it.id] = new PlayerItem(it, quantity);
        }
    }

    public void clearInventory() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void refresh() {
        clearInventory();
        storeItems();
        if (inventoryDisplay != null) {
            inventoryDisplay.loadItems(playerItems);
        }
    }

    private void storeItems() {
        List<string> jsonElements = new();
        foreach (var item in playerItems.Keys) {
            jsonElements.Add(playerItems[item].ToJson());
        }
        String json = String.Join(",", jsonElements);
        json = "[" + json + "]";
        File.WriteAllText(INVENTORY_PATH, json);
    }

    private void _removeItem(Guid id, int quantity) {
        var item = playerItems.GetValueOrDefault(id, null);
        if (item == null) {
            return;
        }
        if (quantity < item.Quantity) {
            item.Quantity -= quantity;
            playerItems[id] = item;
        } else if (quantity == item.Quantity) {
            playerItems.Remove(id);
        } else {
            Debug.Log("Cannot consume item!");
        }
        refresh();
    }

    private void _addItem(Guid id, int quantity) {
        if (playerItems.ContainsKey(id)) {
            var item = playerItems[id];
            item.Quantity += quantity;
            playerItems[id] = item;
        } else {
            Item it = ItemDatabase.findItem(id);
            playerItems[id] = new PlayerItem(it, quantity);
        }
        refresh();
    }

    private void _showItem(Guid id) {
        var item = playerItems.GetValueOrDefault(id, null);
        if (item == null) {
            return;
        }
        item.showItem();
        refresh();
    }

    private void _notShowItem(Guid id) {
        var item = playerItems.GetValueOrDefault(id, null);
        if (item == null) {
            return;
        }
        item.notShow();
        refresh();
    }

    public static void AddItem(Guid id, int quantity) {
        instance._addItem(id, quantity);
    }

    public static void removeItem(Guid id, int quantity) {
        instance._removeItem(id, quantity);
    }

    public static void notShowItem(Guid id) {
        instance._notShowItem(id);
    }

    public static void showItem(Guid id) {
        instance._showItem(id);
    }
}

