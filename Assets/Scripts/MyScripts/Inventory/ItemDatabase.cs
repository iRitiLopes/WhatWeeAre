using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class ItemDatabase : MonoBehaviour {
    public static ItemDatabase instance;
    readonly List<Item> items = new();

    public static bool isInitialized = false;

    public void buildDatabase() {
        JArray data = JArray.Parse(File.ReadAllText(Application.streamingAssetsPath + "/items.json"));
        foreach (JObject item in data) {
            string name = (string?)item.GetValue("name");
            string id = (string)item.GetValue("id");
            bool isRaw = (bool)item.GetValue("isRaw");
            string description = (string?)item.GetValue("description");
            
            Item it = new(id, name, description, isRaw, name);
            it = localize(it);
            if (!isRaw) {
                JArray rawItemJArray =
                    JArray.Parse(item.GetValue("rawItems").ToString());
                List<RawItem> rawItems = new();
                foreach (JObject rawItem in rawItemJArray) {
                    string rawId = (string?)rawItem.GetValue("id");
                    int quantity = (int)rawItem.GetValue("quantity");
                    RawItem r = new(rawId, quantity);
                    rawItems.Add(r);
                }
                it.RawItems = rawItems;
            }
            items.Add(it);
        }
    }

    private Item localize(Item it) {
        var item_name = LocalizationSettings.StringDatabase.GetLocalizedString("items", it.id);
        var items_description = LocalizationSettings.StringDatabase.GetLocalizedString("items_description", it.id);
        return new Item(it.id, item_name, items_description, it.IsRaw, it.name);
    }

    internal IEnumerable<Item> ItensWithRaw() {
       return this.items.Where(x => !x.IsRaw && x.RawItems.Count > 0);
    }

    internal static Item findItemByComponents(List<ItemSlot> components) {
        return instance._findItemByComponents(components);
    }

    private Item _findItemByComponents(List<ItemSlot> components) {
        Dictionary<string, ItemSlot> componentsDict = new();
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
                        componentsDict[x.id].quantity >= x.quantity && item.RawItems.Count == components.Count);

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

    public static Item findItem(string id) {
        return instance.items.Find(i => i.id.Equals(id));
    }
}
