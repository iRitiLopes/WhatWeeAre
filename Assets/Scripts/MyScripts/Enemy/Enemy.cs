using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    Animator an;

    SpriteRenderer render;

    public float Velocity = 1f;

    public bool isKnocked = false;

    Vector2 dir = Vector2.zero;

    public bool toLeft = true;

    public int lifes = 1;

    public GameObject particle;

    public float limitWalkingTimeInSeconds = 3.0f;
    float walkingTimeInSeconds = 0;

    Vector3 lastPosition = new();

    public float magnitude = 50;

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

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
        walkingTimeInSeconds += Time.fixedDeltaTime;

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
        if (limitWalkingTimeInSeconds < walkingTimeInSeconds)
        {
            flip();
        }

        if(Math.Abs(lastPosition.magnitude - transform.position.magnitude) < 1e-10){
            flip();
        }

        if (!isKnocked)
        {
            Vector2 vel = rb.velocity;
            vel.x = dir.x * Velocity;
            rb.velocity = vel;
        }
        lastPosition = transform.position;
    }

    private bool isDead()
    {
        return lifes == 0;
    }

    public void flip()
    {
        dir = dir * -1;
        render.flipX = !render.flipX;
        walkingTimeInSeconds = 0;
    }
}
