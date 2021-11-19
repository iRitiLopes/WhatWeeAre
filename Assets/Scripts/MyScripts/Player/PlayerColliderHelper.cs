using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderHelper : MonoBehaviour {

    public bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Platform") == true) {
            Debug.Log("TO TOCANDO");
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        if (coll.CompareTag("Platform") == true) {
            Debug.Log("NAO TO TOCANDO");
            isColliding = false;
        }
    }
}
