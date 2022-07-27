using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour {

    // Start is called before the first frame update
    public float thrust = 12f;

    public float knockTime = 0.5f;


    public SpriteRenderer render;

    void Start() {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate() {


    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            ApplyEffects(other);
        }
        if (other.collider.CompareTag("PlayerColliderBottom")) {
            ApplyEffects(other);
            var enemy = gameObject.GetComponent<Enemy>();
            enemy.decreaseLife();
        }
    }

    void ApplyEffects(Collision2D other) {
        var enemyRigidBody = GetComponent<Rigidbody2D>();

        enemyRigidBody.inertia = 0;
        enemyRigidBody.velocity = new Vector2(0, 0);

        Vector2 position = transform.position;
        Vector2 direction = other.GetContact(0).point - position;
        direction = -direction.normalized;
        enemyRigidBody.AddForce(direction * thrust, ForceMode2D.Impulse);
        StartCoroutine(knockCo(enemyRigidBody));
    }

    private IEnumerator knockCo(Rigidbody2D enemy) {
        Debug.Log("Hit");
        var e = GetComponent<Enemy>();
        e.isKnocked = true;
        gameObject.GetComponent<AIPatrol>().mustPatrol = false;
        yield return new WaitForSeconds(knockTime);
        e.isKnocked = false;
        gameObject.GetComponent<AIPatrol>().mustPatrol = true;
    }
}
