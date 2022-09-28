using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AIJump : MonoBehaviour {
    public bool mustJump = false;
    public LayerMask groundLayer;
    public Rigidbody2D rigidbody2D;
    public Transform groundCheckPos;

    public float jumpForce = 1f;


    [Range(0, 1)]
    public float jumpChance;

    public float elapsedTime = 0f;

    private void Awake() {
        mustJump = true;
    }

    private void Update() {
        elapsedTime += Time.deltaTime;
    }

    private void FixedUpdate() {
        mustJump = Physics2D.OverlapCircle(groundCheckPos.position, 0.4f, groundLayer);

        if (isLucky() && mustJump && elapsedTime >= 5f) {
            var aiPatrol = GetComponent<AIPatrol>();
            aiPatrol.disable();
            Jump();
            elapsedTime = 0;
        } else if (Physics2D.OverlapCircle(groundCheckPos.position, 0.4f, groundLayer)) {
            var aiPatrol = GetComponent<AIPatrol>();
            aiPatrol.enable();
        }
    }

    void Jump() {
        rigidbody2D.AddForce(Vector2.up * Mathf.Sqrt(2 * Physics2D.gravity.magnitude * jumpForce) * rigidbody2D.mass, ForceMode2D.Impulse);
    }

    private bool isLucky() {
        System.Random rand = new System.Random();
        var value = rand.NextDouble();
        return jumpChance >= value;
    }

}