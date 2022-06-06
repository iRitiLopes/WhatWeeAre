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

}