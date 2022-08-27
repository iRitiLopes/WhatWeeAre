using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Rigidbody2D rb;

    Animator an;

    public bool isKnocked = false;

    public int lifes = 1;

    public GameObject particle;

    float walkingTimeInSeconds = 0;

    // Use this for initialization
    void Start() {

        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    private void Awake() {
        if (FindObjectOfType<GameManager>().EnemyAlreadyDead(this)) {
            Destroy(gameObject);
        }
    }

    private void Update() {
        an.SetBool("isKnocked", isKnocked);
    }

    internal void decreaseLife() {
        lifes--;
    }


    private void FixedUpdate() {
        walkingTimeInSeconds += Time.fixedDeltaTime;

        if (isDead()) {
            var drop = GetComponent<DropItem>();
            if (drop) {
                drop.drop(transform.position);
                FindObjectOfType<GameManager>().KillEnemy(this);
            }
            Instantiate(particle,
            new Vector3(transform.position.x, transform.position.y, 0),
            transform.rotation);
            Destroy(this.gameObject);
        }

    }

    private bool isDead() {
        return lifes <= 0;
    }

    public string hash() {
        return gameObject.name;
    }
}
