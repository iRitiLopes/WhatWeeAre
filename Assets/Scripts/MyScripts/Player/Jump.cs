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

    public bool jumpKeyHeld;
    public bool isJumping = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        bottomHelper = GameObject.FindGameObjectWithTag("PlayerColliderBottom").GetComponent<PlayerColliderHelper>();
        bottomHelper.subscribe(this);
    }

    private float jump() {
        jumpCount++;
        return Mathf.Sqrt(2 * Physics2D.gravity.magnitude * jumpForce);
    }

    // Update is called once per frame
    void Update() {
        if(FindObjectOfType<GameManager>().isPaused){
            return;
        }

        if (GetComponent<Player>().IsDead()) {
            return;
        }

        // Keep apply jump force while space is pressed
        if(Input.GetKeyDown(KeyCode.Space)) {
            isJumping = true;
            if(isOnFloor() || (doubleJump && jumpCount < 2)){
                Debug.Log("Jump");
                jumpKeyHeld = true;
                //rb.AddForce(Vector2.up * jump() * rb.mass, ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, jump());
                an.SetBool("Jump", true);
            } else if (doubleJump && jumpCount < 2) {
                Debug.Log("Double Jump");
                jumpKeyHeld = true;
                //rb.AddForce(Vector2.up * jump() * rb.mass, ForceMode2D.Impulse);
                rb.velocity = new Vector2(rb.velocity.x, jump());
                an.SetBool("Jump", true);
            }
        } else if (Input.GetKeyUp(KeyCode.Space)) {
                Debug.Log("Stop Jump");
                jumpKeyHeld = false;
                isJumping = false;
        } 
        
        {
            an.SetBool("Jump", isOnFloor() == false);
            an.SetBool("Walk", isOnFloor());
        }

        if(isJumping && rb.velocity.y > 0){
            rb.AddForce(Vector2.up * jumpForce * 2 * Time.deltaTime, ForceMode2D.Force);
        }
    }

    public bool isOnFloor() {
        return bottomHelper.isColliding;
    }

    public void notify(GameObject go) {
        if(isOnFloor()){
            
        }
    }

    void resetJumps(){
        jumpCount = 0;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Platform")){
            Debug.Log("On Floor");
            resetJumps();
        }
    }
}
