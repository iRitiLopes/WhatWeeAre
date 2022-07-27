using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AIPatrol : MonoBehaviour {
    public float walkSpeed;
    public bool mustPatrol = true;
    private bool mustTurn;

    public  Rigidbody2D rigidbody2D;
    public Transform groundCheckPos;

    public BoxCollider2D bodyCollider;
    public LayerMask groundLayer;

    private void Awake() {
        mustPatrol = true;
    }

    private void Update() {
        if (mustPatrol){
            Patrol();
        }
    }

    private void FixedUpdate() {
        if(mustPatrol){
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.4f, groundLayer);
        }
    }

    private void Patrol() {
        if(mustTurn || bodyCollider.IsTouchingLayers(groundLayer)){
            Flip();
        }
        rigidbody2D.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
    }

    void Flip(){
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}