using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Animator))]

public class SoyBoyController : MonoBehaviour
{
    public float speed = 14.0f;
    public float accel = 6.0f;

    private Vector2 input;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        //1
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Jump");
        //2
        if (input.x >0.0f)
        {
            sr.flipX = false;
        }
        else if (input.x < 0.0f)
        {
            sr.flipX = true;
        }

    }
    void FixedUpdate()
    {
        //1
        var acceleration = accel;
        var xVelocity = 0.0f;
        //2
        if (input.x == 0 )
        {
            xVelocity = 0.0f;
        }
        else
        {
            xVelocity = rb.velocity.x;
        }

        //3
        rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));

        //4
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

}

