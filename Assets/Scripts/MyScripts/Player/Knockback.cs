using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float thrust = 12f;
    public float knockTime = 0.5f;
    public bool left;
    public bool right;
    public bool bottom;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.CompareTag("Enemy")){
            audioSource.Play();
            var playerRigidBody = GetComponentInParent<Rigidbody2D>();
            var player = GetComponentInParent<Player>();

            if(player.isDead()){
                return;
            }

            playerRigidBody.isKinematic = false;

            Vector2 direction;
            if(left){
                StartCoroutine(GetComponentInParent<Invulnerable>().invulnerable());
                player.decreaseLife();
            }else if(right){
                StartCoroutine(GetComponentInParent<Invulnerable>().invulnerable());
                player.decreaseLife();
            }

            direction = other.GetContact(0).normal * other.GetContact(0).normalImpulse ;

            playerRigidBody.AddForce(direction, ForceMode2D.Impulse);
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
