using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase : MonoBehaviour {
    private static ItemDatabase instance;

    List<Item> items = new List<Item>();

    public static bool isInitialized = false;

    public void buildDatabase() {
        JArray data =
            JArray.Parse(File.ReadAllText("./Assets/Sprites/items/items.json"));
        foreach (JObject item in data) {
            string name = ((string?)item.GetValue("name"));
            Guid id = Guid.Parse((string)item.GetValue("id"));
            bool isRaw = (bool)item.GetValue("isRaw");
            string description = (string?)item.GetValue("description");
            Item it = new Item(id, name, description, isRaw);
            if (!isRaw) {
                JArray rawItemJArray =
                    JArray.Parse(item.GetValue("rawItems").ToString());
                List<RawItem> rawItems = new List<RawItem>();
                foreach (JObject rawItem in rawItemJArray) {
                    Guid rawId = Guid.Parse((string?)rawItem.GetValue("id"));
                    int quantity = (int)rawItem.GetValue("quantity");
                    RawItem r = new RawItem(rawId, quantity);
                    rawItems.Add(r);
                }
                it.RawItems = rawItems;
            }
            items.Add(it);
        }
    }

    internal static Item findItemByComponents(List<ItemSlot> components) {
        return instance._findItemByComponents(components);
    }

    private Item _findItemByComponents(List<ItemSlot> components) {
        Dictionary<Guid, ItemSlot> componentsDict =
            new Dictionary<Guid, ItemSlot>();
        foreach (var component in components) {
            componentsDict.Add(component.item.id, component);
        }

        foreach (var item in items) {
            if (item.IsRaw) {
                continue;
            }
            var allMatch = false;
            allMatch = item.RawItems.All(x =>
                        componentsDict.ContainsKey(x.id) &&
                        componentsDict[x.id].quantity >= x.quantity);

            if (allMatch) {
                return item;
            }
        }
        return null;
    }

    private void Awake() {
        buildDatabase();
        if (instance == null || instance != this) {
            instance = this;
            isInitialized = true;
        }
    }

    public int size() {
        return this.items.Count;
    }

    public static Item findItem(Guid id) {
        return instance.items.Find(i => i.id.Equals(id));
    }
}
