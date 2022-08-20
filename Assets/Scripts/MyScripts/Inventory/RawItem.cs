using System;
using UnityEngine;

[Serializable]
public class RawItem {

    [SerializeField]
    public String id;
    public int quantity;


    public RawItem(String id, int quantity) {
        this.id = id;
        this.quantity = quantity;
    }

    public RawItem(RawItem item) {
        this.id = item.id;
        this.quantity = item.quantity;
    }

}

