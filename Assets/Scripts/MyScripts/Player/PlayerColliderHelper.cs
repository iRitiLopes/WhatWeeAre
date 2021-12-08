using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderHelper : MonoBehaviour {

    public bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("to no chao");
        if(coll.CompareTag("Platform")){
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        Debug.Log("sai do chao");
        if(coll.CompareTag("Platform")){
            isColliding = false;
        }
    }
}
