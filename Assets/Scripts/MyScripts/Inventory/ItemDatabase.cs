using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

namespace Inventory {
    public class ItemDatabase : MonoBehaviour {
        List<Item> items = new List<Item>();

        void buildDatabase() {
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
            buildDatabase();
        }

        public int size(){
            return this.items.Count;
        }

    }
}
