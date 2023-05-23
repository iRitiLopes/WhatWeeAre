using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUpDisplay : MonoBehaviour {
    public PowerUpEffect powerUpEffect = null;
    public Action onCollect;

    private IEnumerator Start() {
        yield return new WaitUntil(() => ItemDatabase.isInitialized);

        //gameObject.GetComponent<SpriteRenderer>().sprite = item.spriteInGame;

        if(FindObjectOfType<GameManager>().AlreadyCollected(this)){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            if (powerUpEffect != null) {
                powerUpEffect.Apply(coll.gameObject, FindObjectOfType<PlayerPowerUp>());
                gameObject.GetComponent<DialogueTrigger>()?.TriggerDialogue();
            }
            FindObjectOfType<GameManager>().CollectItem(this);
            Destroy(gameObject);
        }
    }

    public string hash() {
        return powerUpEffect.name + "_" + transform.position.x.ToString() + "_" + transform.position.y.ToString();
    }

}