using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderHelper : MonoBehaviour {

    public bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D coll) {
        isColliding = coll.CompareTag("Platform");
    }

    private void OnTriggerExit2D(Collider2D coll) {
        isColliding = !coll.CompareTag("Platform");
    }
}
