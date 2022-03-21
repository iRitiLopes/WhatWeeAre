using UnityEngine;
using System;
using Inventory;
using System.Collections;
using System.Collections.Generic;
namespace Inventory {
    public class Item {

        static string basePath = "./Assets/Sprites/items/";
        public Guid id;
        public string name;
        public string description;
        public Sprite icon; 
        bool isRaw;

        List<RawItem> rawItems;

        public List<RawItem> RawItems { get => rawItems; set => rawItems = value; }
        public bool IsRaw { get => isRaw; }

        public Item(string name, string description, bool isRaw) {
            this.id = Guid.NewGuid();
            this.name = name;
            this.description = description;
            this.isRaw = isRaw;
            this.icon = Resources.Load<Sprite>(basePath + name + ".png");
        }

        public Item(Guid id, string name, string description, bool isRaw) {
            this.id = id;
            this.name = name;
            this.description = description;
            this.isRaw = isRaw;
            this.icon = Img2Sprite.LoadNewSprite(basePath + name + ".png");
        }

        public Item(Item item) {
            this.id = item.id;
            this.name = item.name;
            this.description = item.description;
            this.icon = Resources.Load<Sprite>(basePath + item.name + ".png");
        }

    }
}
