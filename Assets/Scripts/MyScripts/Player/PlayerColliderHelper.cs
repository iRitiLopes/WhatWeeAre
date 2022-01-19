using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderHelper : MonoBehaviour {

    public bool isColliding = false;

    private void OnCollisionEnter2D(Collision2D coll) {
        if(coll.collider.CompareTag("Platform")){
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D coll) {
        if(coll.collider.CompareTag("Platform")){
            isColliding = false;
        }
    }
}
