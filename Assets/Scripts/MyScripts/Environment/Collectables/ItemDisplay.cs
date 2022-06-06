using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class ItemDisplay : MonoBehaviour {
    public CollectableItem collectableItem;
    public Item item;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => ItemDatabase.isInitialized);
        item = ItemDatabase.findItem(Guid.Parse(collectableItem.id));
        
        gameObject.GetComponent<SpriteRenderer>().sprite = item.spriteInGame;
    }

    internal void collect() {
        Destroy(this.gameObject);
    }

    public static void create(Vector3 position, GameObject collectableItem){
        var x = PrefabUtility.InstantiatePrefab(collectableItem) as GameObject;
        x.name = "DropableItem_";
        x.transform.position = position;
    }
}