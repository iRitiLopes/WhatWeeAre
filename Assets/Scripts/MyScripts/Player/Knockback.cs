using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float thrust = 12f;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            var playerRigidBody = GetComponentInParent<Rigidbody2D>();
            //playerRigidBody.isKinematic = false;
            Vector2 difference = playerRigidBody.transform.position - transform.position;
            difference = Vector2.up.normalized * thrust;
            playerRigidBody.AddForce(difference, ForceMode2D.Impulse);
            //playerRigidBody.isKinematic = true;
        }
    }
}
