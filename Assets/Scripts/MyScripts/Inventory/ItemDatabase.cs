using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.UI;
using System;

namespace Inventory {
    public class ItemDatabase : MonoBehaviour {

        private static ItemDatabase instance;
        List<Item> items = new List<Item>();
        public bool isInitialized = false;
        public void buildDatabase() {
            JArray data = JArray.Parse(File.ReadAllText("./Assets/Sprites/items/items.json"));
            foreach (JObject item in data) {
                string name = ((string?)item.GetValue("name"));
                Guid id = Guid.Parse((string?)item.GetValue("id"));
                bool isRaw = (bool)item.GetValue("isRaw");
                string description = (string?)item.GetValue("description");
                Item it = new Item(id, name, description, isRaw);
                if (!isRaw) {
                    JArray rawItemJArray = JArray.Parse(item.GetValue("rawItems").ToString());
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

        private void Awake() {
        buildDatabase();
        instance = this;
    }

        public int size() {
            return this.items.Count;
        }

        public static Item findItem(Guid id) {
            return instance.items.Find(i => i.id.Equals(id));
        }
    }
}
