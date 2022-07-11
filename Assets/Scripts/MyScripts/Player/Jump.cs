using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour, Notificable {
    public float jumpForce = 2.5f;

    public Rigidbody2D rb { get; private set; }
    public Animator an { get; private set; }
    public SpriteRenderer render { get; private set; }

    [SerializeField]
    bool doubleJump = false;

    [SerializeField]
    int jumpCount = 0;

    internal void enableDoubleJump() {
        this.doubleJump = true;
    }

    public PlayerColliderHelper bottomHelper { get; private set; }

    bool jumpKeyHeld;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        bottomHelper = GameObject.FindGameObjectWithTag("PlayerColliderBottom").GetComponent<PlayerColliderHelper>();
        bottomHelper.subscribe(this);
    }

    private float jump() {
        return Mathf.Sqrt(2 * Physics2D.gravity.magnitude * jumpForce);
    }

    // Update is called once per frame
    void Update() {

        if (GetComponent<Player>().IsDead()) {
            return;
        }

        if (isOnFloor() || (doubleJump && jumpCount < 2)) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                ++jumpCount;
                jumpKeyHeld = true;
                rb.AddForce(Vector2.up * jump() * rb.mass, ForceMode2D.Impulse);
                an.SetBool("Jump", true);
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
        return bottomHelper.isColliding;
    }

    public void notify(GameObject go) {
        var isOnFloor = go.GetComponent<PlayerColliderHelper>().isColliding;
        if(isOnFloor){
            resetJumps();
        }
    }

    void resetJumps(){
        jumpCount = 0;
    }
}
