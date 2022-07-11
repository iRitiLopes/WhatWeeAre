using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUpDisplay : MonoBehaviour {
    public PowerUpEffect powerUpEffect = null;

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            if (powerUpEffect != null) {
                Debug.Log("aplicando");
                powerUpEffect.Apply(coll.gameObject);
            }
            Destroy(gameObject);
        }
    }

}