using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ItemDisplay : MonoBehaviour {
    public CollectableItem collectableItem;
    public Item item;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => ItemDatabase.isInitialized);
        item = ItemDatabase.findItem(Guid.Parse(collectableItem.id));

        Debug.Log(gameObject.GetComponent<SpriteRenderer>().sprite);
        
        gameObject.GetComponent<SpriteRenderer>().sprite = item.spriteInGame;
    }
}