using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Rigidbody2D rb;
    Animator an;
    SpriteRenderer render;

    GameControl game;

    PlayerColliderHelper bottomHelper;
    PlayerColliderHelper rightHelper;
    PlayerColliderHelper leftHelper;

    public float Velocity = 1f;
    public float jumpForce;

    public float limitWalk = 0.5f;

    public bool isRightWalking = true;

    Vector2 initialPosition;

    bool jumpKeyHeld;

    // Use this for initialization
    void Start() {
        game = GameObject.Find("Main Camera").GetComponent<GameControl>();

        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        bottomHelper = GetComponentsInChildren<PlayerColliderHelper>()[0];
        rightHelper = GetComponentsInChildren<PlayerColliderHelper>()[1];
        leftHelper = GetComponentsInChildren<PlayerColliderHelper>()[2];

        jumpForce = CalculateJumpForce(Physics2D.gravity.magnitude, 2.5f);

        this.initialPosition = new Vector2(transform.position.x, transform.position.y);
    }

    private void Update() {
        Vector2 dir = Vector2.zero;
        if(rightHelper.isColliding || transform.position.x >= this.initialPosition.x * (1 + limitWalk)){
            isRightWalking = false;
        }
        if(leftHelper.isColliding || transform.position.x <= this.initialPosition.x * (limitWalk)){
            if(leftHelper.isColliding){
                Debug.Log("COLIDI NA ESQUERDA");
            }
            isRightWalking = true;
        }

        if (isRightWalking) {
            dir.x = 1;
            render.flipX = false;
        } else {
            dir.x = -1;
            render.flipX = true;
        }

        Vector2 vel = rb.velocity;
        vel.x = dir.x * Velocity;
        rb.velocity = vel;
    }

    private void FixedUpdate() {

    }


    public float GetAbsRunVelocity() {
        return Mathf.Abs(rb.velocity.x);
    }

    public bool isOnFloor() {
        return bottomHelper.isColliding;
    }

    public bool isDead() {
        return false;
    }

    public static float CalculateJumpForce(float gravityStrength, float jumpHeight) {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

}
