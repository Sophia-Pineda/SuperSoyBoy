using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Animator))]

public class SoyBoyController : MonoBehaviour
{
    public bool isJumping;
    public float speed = 14.0f;
    public float accel = 6.0f;
    public float jumpSpeed = 8.0f;
    public float jumpDurationThreshold = 0.25f;
    public float airAccel = 3.0f;
    public float jump = 14.0f;
 
    private Vector2 input;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    private float rayCastLengthCheck = 0.005f;
    private float width;
    private float height;
    private float jumpDuration;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
    }

    public bool PlayerIsOnGround()
    {
        //1
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        bool groundCheck3 = Physics2D.Raycast(new Vector2(transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);

        if (groundCheck1 || groundCheck2 || groundCheck3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsWallToLeftOrRight()
    {
        //1 
        bool wallOnleft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), - Vector2.right, rayCastLengthCheck);
        bool wallOnRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);

        //2
        if(wallOnleft || wallOnRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PlayerIsTouchingGroundOrWall()
    {
        if (PlayerIsOnGround() || IsWallToLeftOrRight())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetWallDirection()
    {
        bool isWallLeft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        bool isWallRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);

        if(isWallLeft)
        {
            return -1;

        }
        else
        {
            return 0;
        }
    }

    void Update()
    {
        //1
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Jump");

        animator.SetFloat("Speed", Mathf.Abs(input.x));
        //2
        if (input.x >0.0f)
        {
            sr.flipX = false;
        }
        else if (input.x < 0.0f)
        {
            sr.flipX = true;
        }

        if (input.y >= 1.0f)
        {
            jumpDuration += Time.deltaTime; //Changed PG370
            animator.SetBool("IsJumping", true);
        }
        else
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            jumpDuration = 0.0f;
        }

        if (PlayerIsOnGround() && isJumping == false)
        {
            if (input.y > 0.0f)
            {
                isJumping = true;

            }
            animator.SetBool("IsOnWall", false);
        }

        if (jumpDuration > jumpDurationThreshold)
            input.y = 0.0f;

    }

    void FixedUpdate()
    {
        //1
             //var acceleration = accel; is replaced with:
        var acceleration = 0.0f;
        if( PlayerIsOnGround())
        {
            acceleration = accel;
        }
        else
        {
            acceleration = airAccel;
        }

        var xVelocity = 0.0f;
        //2

        if (PlayerIsOnGround() && input.x == 0)
        {
            xVelocity = 0.0f;
        }
        else
        {
            xVelocity = rb.velocity.x;
        }

        var yVelocity = 0.0f;

        if (PlayerIsTouchingGroundOrWall() && input.y == 1)
        {
            yVelocity = jump;
        }
        else
        {
            yVelocity = rb.velocity.y;
        }

        //3
        rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));

        //4
        rb.velocity = new Vector2(xVelocity, yVelocity);  // PG 365

        //if(IsWallToLeftOrRight() && !PlayerIsOnGround() && input.y == 1)
        //{
        //    rb.velocity = new Vector2(-GetWallDirection() * speed * 0.75f, rb.velocity.y);   //replaced on PG 370
        //}
        if (IsWallToLeftOrRight() && !PlayerIsOnGround() && input.y == 1)
        {
            rb.velocity = new Vector2(-GetWallDirection() * speed * 0.75f, rb.velocity.y);
            animator.SetBool("IsOnWall", false);
            animator.SetBool("IsJumping", true);
        }
        else if (!IsWallToLeftOrRight())
        {
            animator.SetBool("IsOnWall", false);
            animator.SetBool("IsJumping", true);

        }
        if (IsWallToLeftOrRight() && !PlayerIsOnGround())
        {
            animator.SetBool("IsOnWall", true);
        }


        if (isJumping && jumpDuration < jumpDurationThreshold)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

}

