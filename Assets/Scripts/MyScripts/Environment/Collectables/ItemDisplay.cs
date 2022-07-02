using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemDisplay : MonoBehaviour {
    public CollectableItem collectableItem;
    public Item item;

    public PowerUpEffect powerUpEffect = null;

    private IEnumerator Start() {
        yield return new WaitUntil(() => ItemDatabase.isInitialized);
        item = ItemDatabase.findItem(Guid.Parse(collectableItem.id));

        gameObject.GetComponent<SpriteRenderer>().sprite = item.spriteInGame;
    }

    internal void collect() {
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            Debug.Log(coll.gameObject.name);
            if(powerUpEffect != null){
                Debug.Log("aplicando");
                powerUpEffect.Apply(coll.gameObject);
            }
            Destroy(gameObject);
        }
    }

}