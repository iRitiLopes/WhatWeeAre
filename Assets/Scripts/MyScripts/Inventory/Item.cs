using UnityEngine;
using System;


namespace Inventory {
    class Item {

        static string basePath = "./Assets/Sprites/items/";
        Guid id;
        string name;
        string description;
        Sprite icon;
        bool isRaw;


        public Item(string name, string description, bool isRaw) {
            this.id = Guid.NewGuid();
            this.name = name;
            this.description = description;
            this.isRaw = isRaw;
            this.icon = Resources.Load<Sprite>(basePath + name);
        }

        public Item(Guid id, string name, string description, bool isRaw) {
            this.id = id;
            this.name = name;
            this.description = description;
            this.isRaw = isRaw;
            this.icon = Resources.Load<Sprite>(basePath + name);
        }

        public Item(Item item) {
            this.id = item.id;
            this.name = item.name;
            this.description = item.description;
            this.icon = Resources.Load<Sprite>(basePath + item.name);
        }

    }
}
