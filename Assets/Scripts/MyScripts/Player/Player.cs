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

    bool showInventory = false;
    GameObject inventoryUI;

    // Use this for initialization
    void Start() {
        inventoryUI = GameObject.Find("Inventory");
        inventoryUI.SetActive(false);
        game = GameObject.Find("Main Camera").GetComponent<GameControl>();

        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        bottomHelper = GetComponentsInChildren<PlayerColliderHelper>()[0];
        rightHelper = GetComponentsInChildren<PlayerColliderHelper>()[1];
        leftHelper = GetComponentsInChildren<PlayerColliderHelper>()[2];

    }

    private void Update() {

        if (isDead()) {
            an.SetBool("Dead", true);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>().reloadLevel();
            return;
        } else {
            an.SetBool("Dead", false);
        }


        if (showInventory) {
            inventoryUI.SetActive(true);
        } else {
            inventoryUI.SetActive(true);
         }

        if (Input.GetKey(KeyCode.I)) {
            showInventory = true;
        } else {
            showInventory = false;
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

    private void FixedUpdate() {

        if (game.IsGameRuning == false) {
            an.SetFloat("Velocity", 0);
            return;
        }
    }

    public void decreaseLife() {
        if (lifes >= 0)
            lifes--;
    }

    public float GetAbsRunVelocity() {
        return Mathf.Abs(rb.velocity.x);
    }

    public bool isDead() {
        return lifes <= 0;
    }


}
