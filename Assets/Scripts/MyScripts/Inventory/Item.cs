using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item {

    static string spritesBasePath = Application.streamingAssetsPath + "/Sprites/items/ingame_sprites/";
    public string id;
    public string name;

    public string spriteName;
    public string description;
    public Sprite icon;
    public Sprite spriteInGame;
    bool isRaw;

    List<RawItem> rawItems = new();

    public List<RawItem> RawItems { get => rawItems; set => rawItems = value; }
    public bool IsRaw { get => isRaw; }

    public Item(string id, string name, string description, bool isRaw, string spriteName) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.isRaw = isRaw;
        this.spriteName = spriteName;
        this.icon = Img2Sprite.LoadNewSprite(spritesBasePath + spriteName + ".png");
        this.spriteInGame = Img2Sprite.LoadNewSprite(spritesBasePath + spriteName + ".png");
    }

    public Item(Item item) {
        this.id = item.id;
        this.name = item.name;
        this.description = item.description;
        this.icon = Resources.Load<Sprite>(spritesBasePath + item.spriteName + ".png");
        this.spriteInGame = Img2Sprite.LoadNewSprite(spritesBasePath + item.spriteName + ".png");
    }

    public override string ToString() {
        return "Item[" + name + "]";
    }

}
