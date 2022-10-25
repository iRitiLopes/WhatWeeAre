using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            Action<Collider2D> action = postFall;
            GameObject
                .FindGameObjectWithTag("MainCamera")
                .GetComponent<GameController>()
                .reloadLevel(postFall, other);
        }
    }

    void postFall(Collider2D other){
        other.GetComponent<Player>().DecreaseLife();
        StartCoroutine(other.GetComponent<Invulnerable>().invulnerable());
    }
}
