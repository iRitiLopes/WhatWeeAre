using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject {
    public string powerUpName;

    public Sprite sprite;

    public bool canBeCreated = false;

    public RawItem[] rawItems;

    public string description;
    public abstract void Apply(GameObject target, PlayerPowerUp playerPowerUp);

    public bool canCreate(List<ItemSlot> items){
        Dictionary<string, RawItem> componentsDict = new();
        foreach (var component in rawItems) {
            componentsDict.Add(component.id, component);
        }

        foreach (var item in items) {
            if (componentsDict.ContainsKey(item.item.id) && componentsDict[item.item.id].quantity <= item.quantity) {
                componentsDict.Remove(item.item.id);
            }
        }
        return componentsDict.Count() == 0;
    }
}
