using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.UI;
using System;

namespace Inventory {
    public class ItemDatabase : MonoBehaviour {
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
                items.Add(it);
            }
        }

        private void Start() {
            isInitialized = true;
        }

        public int size() {
            return this.items.Count;
        }

        public Item findItem(Guid id){
            return this.items.Find( i => i.id.Equals(id));
        }
    }
}
