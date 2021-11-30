using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    public float jumpForce = 2.5f;

    public Rigidbody2D rb { get; private set; }
    public Animator an { get; private set; }
    public SpriteRenderer render { get; private set; }
    public PlayerColliderHelper bottomHelper { get; private set; }

    bool jumpKeyHeld;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        bottomHelper = GetComponentsInChildren<PlayerColliderHelper>()[0];
    }

    private float jump() {
        return Mathf.Sqrt(2 * Physics2D.gravity.magnitude * jumpForce);
    }

    // Update is called once per frame
    void Update() {

        if (GetComponent<Player>().isDead()) {
            return;
        }

        if (isOnFloor()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                jumpKeyHeld = true;
                rb.AddForce(Vector2.up * jump() * rb.mass, ForceMode2D.Impulse);
            } else if (Input.GetKeyUp(KeyCode.Space)) {
                jumpKeyHeld = false;
            } else {
                an.SetBool("Jump", false);
                an.SetBool("Walk", true);
            }
        } else {
            an.SetBool("Jump", true);
            an.SetBool("Walk", false);
            if (!jumpKeyHeld && Vector2.Dot(rb.velocity, Vector2.up) > 0) {
                rb.AddForce(Vector2.down * rb.mass * Physics2D.gravity.magnitude);
            }
        }
    }

    public bool isOnFloor() {
        return GameObject.FindGameObjectWithTag("PlayerColliderBottom").GetComponent<PlayerColliderHelper>().isColliding;
    }

}
