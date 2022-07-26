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

        if(FindObjectOfType<GameManager>().AlreadyCollected(this)){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            Inventory.AddItem(item.id, 1);
            coll.gameObject.GetComponent<AudioSource>().PlayOneShot(collectableItem.audioClip);
            if(powerUpEffect != null){
                powerUpEffect.Apply(coll.gameObject);
            }
            FindObjectOfType<GameManager>().CollectItem(this);
            Destroy(gameObject);
        }
    }

    public string hash() {
        return item.name + "_" + transform.position.x.ToString() + "_" + transform.position.y.ToString();
    }
}