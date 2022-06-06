using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public Vector2 direction;

    // Start is called before the first frame update
    public float thrust = 12f;

    public float knockTime = 0.5f;


    public SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.render.flipX)
        {
            direction = Vector2.right * thrust;
        }
        else
        {
            direction = Vector2.left * thrust;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            var enemyRigidBody = GetComponent<Rigidbody2D>();

            enemyRigidBody.isKinematic = false;
            direction = other.GetContact(0).normal * other.GetContact(0).normalImpulse;
            enemyRigidBody.AddForce(direction, ForceMode2D.Impulse);
            StartCoroutine(knockCo(enemyRigidBody));
        }
        if (other.collider.CompareTag("PlayerColliderBottom"))
        {
            var enemy = this.gameObject.GetComponent<Enemy>();
            enemy.decreaseLife();
        }
    }

    private IEnumerator knockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            var e = GetComponent<Enemy>();
            e.isKnocked = true;
            yield return new WaitForSeconds(knockTime);
            enemy.isKinematic = true;
            e.isKnocked = false;
        }
    }
}
