using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float thrust = 12f;
    public float knockTime = 0.5f;
    public bool left;
    public bool right;
    public bool bottom;
    public bool top;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Enemy")) {

            if (GetComponentInParent<Invulnerable>().isInvulnerable) {
                return;
            }

            audioSource.Play();
            var playerRigidBody = GetComponentInParent<Rigidbody2D>();
            var player = GetComponentInParent<Player>();

            if (player.IsDead()) {
                return;
            }

            Vector2 direction;
            if (left || right || top) {
                StartCoroutine(GetComponentInParent<Invulnerable>().invulnerable());
                player.DecreaseLife();
            }

            direction = other.GetContact(0).normal * other.GetContact(0).normalImpulse;

            playerRigidBody.inertia = 0;
            playerRigidBody.velocity = new Vector2(0, 0);
            var force = direction * thrust;
            playerRigidBody.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(knockCo(playerRigidBody));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Enemy")) {
            return;
        }

        if (GetComponentInParent<Invulnerable>().isInvulnerable) {
            return;
        }

        if (left || right || top) {
            audioSource.Play();
            var player = GetComponentInParent<Player>();
            StartCoroutine(GetComponentInParent<Invulnerable>().invulnerable());
            player.DecreaseLife();
        }
    }

    private IEnumerator knockCo(Rigidbody2D player) {
        if (player != null) {
            var e = GetComponentInParent<Player>();
            e.isKnocked = true;
            yield return new WaitForSeconds(knockTime);
            e.isKnocked = false;
        }
    }
}
