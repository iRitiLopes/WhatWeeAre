using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;
    Animator an;
    SpriteRenderer render;

    GameControl game;

    PlayerColliderHelper bottomHelper;
    PlayerColliderHelper rightHelper;
    PlayerColliderHelper leftHelper;

    public float Velocity = 1f;
    public int lifes = 3;
    public bool isKnocked = false;


    // Use this for initialization
    void Start() {
        game = GameObject.Find("Main Camera").GetComponent<GameControl>();

        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        bottomHelper = GetComponentsInChildren<PlayerColliderHelper>()[0];
        rightHelper = GetComponentsInChildren<PlayerColliderHelper>()[1];
        leftHelper = GetComponentsInChildren<PlayerColliderHelper>()[2];

    }

    private void Update() {
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
        }else{
        }

        an.SetFloat("Velocity", GetAbsRunVelocity());

        if (rb.velocity.x > 0) {
            render.flipX = false;
        } else if (rb.velocity.x < 0) {
            render.flipX = true;
        }


    }

    private void FixedUpdate() {

        if (game.IsGameRuning == false) {
            an.SetFloat("Velocity", 0);
            return;
        }
    }

    public float GetAbsRunVelocity() {
        return Mathf.Abs(rb.velocity.x);
    }

    public bool isDead() {
        return false;
    }



}
