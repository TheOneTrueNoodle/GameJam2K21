﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; //Player speed
    public float jumpMagnitute; //How hard they jump
    private float moveInput; // Player input axis!

    private Rigidbody2D rb; // The players rigidbody

    private bool facingRight = true; //Whether or not you're facing right (for flipping sprite)


    private bool isGrounded; //Check for if the player is on the ground
    public Transform groundCheck; // Where to check for that ^
    public float checkRadius; // Radius of that check ^
    public LayerMask whatIsGround; // What to look for in that check ^^


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal"); //Take in horizontal input from player
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); //Move player

        //Flip the spite so you run the other way
        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        //Jump!
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpMagnitute;
            isGrounded = false;
        }
    }

    void Flip()
    {
        //Flip the scale
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}