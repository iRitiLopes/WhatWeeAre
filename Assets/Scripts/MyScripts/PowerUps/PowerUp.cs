using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField]
    PowerUpEffect powerUpEffect;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            PlayerPowerUp playerPowerUp = coll.gameObject.GetComponent<PlayerPowerUp>();
            playerPowerUp.AddPowerUp(powerUpEffect);
        }
    }
}
