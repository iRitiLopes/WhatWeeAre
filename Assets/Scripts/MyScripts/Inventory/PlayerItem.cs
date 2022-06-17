using System;
using UnityEngine;

[Serializable]
public class PlayerItem {
    [NonSerialized]
    Item item;
    [SerializeField]
    public String id;
    [SerializeField]
    public int quantity;

    [NonSerialized]
    public bool show = true;

    public PlayerItem(Item item, int qt){
        this.Item = item;
        this.quantity = qt;
        this.id = this.Item.id.ToString();
    }

    public Item Item { get => item; private set => item = value; }
    public int Quantity { get => quantity; set => quantity = value; }

    public String ToJson(){
        return JsonUtility.ToJson(this);
    }

    public void notShow(){
        this.show = false;
    }

    public void showItem(){
        this.show = true;
    }
}