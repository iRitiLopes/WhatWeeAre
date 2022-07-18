using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {

    SpriteRenderer render;
    private Rigidbody2D rb;
    private Animator an;
    public bool isKnocked = false;
    public float Velocity = 1f;

    private void Start() {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    internal void IncreaseSpeed(float amount, float seconds) {
        StartCoroutine(IncreaseAndDecreaseSpeed(amount, seconds));
    }

    private IEnumerator IncreaseAndDecreaseSpeed(float amount, float seconds){
        Velocity += amount;
        yield return new WaitForSeconds(seconds);
        Velocity -= amount;
    }


    private void Update() {
        if(FindObjectOfType<GameManager>().isPaused){
            return;
        }
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) {
            dir.x = -1;
        } else if (Input.GetKey(KeyCode.D)) {
            dir.x = 1;
        }

        if (!isKnocked) {
            Vector2 vel = rb.velocity;
            vel.x = dir.x * Velocity;
            rb.velocity = vel;
        } else {
        }

        an.SetFloat("Velocity", GetAbsRunVelocity());

        if (rb.velocity.x > 0) {
            render.flipX = false;
        } else if (rb.velocity.x < 0) {
            render.flipX = true;
        }
    }

    public float GetAbsRunVelocity() {
        return Mathf.Abs(rb.velocity.x);
    }
}