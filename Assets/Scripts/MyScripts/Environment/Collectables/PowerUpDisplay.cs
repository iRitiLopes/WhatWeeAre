using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUpDisplay : MonoBehaviour {
    public PowerUpEffect powerUpEffect = null;
    public Action onCollect;

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            if (powerUpEffect != null) {
                powerUpEffect.Apply(coll.gameObject, FindObjectOfType<PlayerPowerUp>());
                gameObject.GetComponent<DialogueTrigger>()?.TriggerDialogue();
            }
            Destroy(gameObject);
        }
    }


}