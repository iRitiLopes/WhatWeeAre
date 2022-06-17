using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    Animator an;

    SpriteRenderer render;

    GameControl game;

    public float Velocity = 1f;

    public float limitWalk = 0.5f;

    public bool isKnocked = false;

    Vector2 initialPosition;

    Vector2 dir = Vector2.zero;

    public bool toLeft = true;

    public int lifes = 1;

    public GameObject particle;

    // Use this for initialization
    void Start()
    {
        game = GameObject.Find("Main Camera").GetComponent<GameControl>();

        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        this.initialPosition =
            new Vector2(transform.position.x, transform.position.y);

        dir = Vector2.right;
    }

    private void Update()
    {
        an.SetBool("isKnocked", isKnocked);
    }

    internal void decreaseLife()
    {
        lifes--;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        toLeft = !toLeft;
    }

    private void FixedUpdate()
    {
        if (isDead())
        {
            var drop = GetComponent<DropItem>();
            if (drop)
            {
                drop.drop(transform.position);
            }
            Instantiate(particle,
            new Vector3(transform.position.x, transform.position.y, 0),
            transform.rotation);
            Destroy(this.gameObject);
        }
        if (transform.position.x >= (this.initialPosition.x * (1 + limitWalk)))
        {
            flip();
        }
        if (
            transform.position.x <=
            (this.initialPosition.x - this.initialPosition.x * limitWalk)
        )
        {
            flip();
        }

        if (!isKnocked)
        {
            Vector2 vel = rb.velocity;
            vel.x = dir.x * Velocity;
            rb.velocity = vel;
        }
    }

    private bool isDead()
    {
        return lifes == 0;
    }

    public void flip()
    {
        dir = dir * -1;
        render.flipX = !render.flipX;
    }
}
