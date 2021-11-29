using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float thrust = 12f;
    public float knockTime = 0.5f;
    public bool left;
    public bool right;
    public bool bottom;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            var playerRigidBody = GetComponentInParent<Rigidbody2D>();
            playerRigidBody.isKinematic = false;
            Vector2 difference = playerRigidBody.transform.position - transform.position;

            Vector2 direction;
            if(left){
                direction = Vector2.right.normalized + Vector2.up.normalized;
            }else if(right){
                direction = Vector2.left.normalized + Vector2.up.normalized;
            }else if(bottom){
                direction = Vector2.up.normalized;
            }else{
                direction = Vector2.zero.normalized;
            }

            difference = direction * thrust;


            playerRigidBody.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(knockCo(playerRigidBody));
        }
    }

    private IEnumerator knockCo(Rigidbody2D player){
        if(player != null){
            var e = GetComponentInParent<Player>();
            e.isKnocked = true;
            yield return new WaitForSeconds(knockTime);
            player.isKinematic = false;
            e.isKnocked = false;
        }
    }
}
