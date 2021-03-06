﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; // Player speed
    private float moddedSpeed; // a changable speed variable
    public int speedDivisor;

    public float jumpMagnitute; // How hard they jump
    private float moveInput; // Player input axis!

    private Rigidbody2D rb; // The players rigidbody

    private bool facingRight = true; //Whether or not you're facing right (for flipping sprite)


    private bool isGrounded; //Check for if the player is on the ground
    public Transform groundCheck; // Where to check for that ^
    public float checkRadius; // Radius of that check ^
    public LayerMask whatIsGround; // What to look for in that check ^^

    public bool holdingSomething;
    public Transform holdSpot;
    public GameObject heldObject;
    public bool throwable = true;

    public int yeetStrength;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal"); //Take in horizontal input from player
        rb.velocity = new Vector2(moveInput * moddedSpeed, rb.velocity.y); //Move player

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
        //Half the speed when you're holding something.
        if(holdingSomething)
        {
            moddedSpeed = speed / speedDivisor;
        }
        else
        {
            moddedSpeed = speed;
        }


        //Jump!
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpMagnitute;
            isGrounded = false;
        }

        //Monitor if the object held can be thrown yet or not
        if(!Input.GetButton("PickupThrow") && !throwable && holdingSomething)
        {
            throwable = true;
        }

        //Throw!!!
        if(Input.GetButton("PickupThrow") && throwable && holdingSomething)
        {
            holdingSomething = false;
            
            //Calculate position to put object you were holding at
            Vector2 camCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 reletiveVector = camCoords - rb.position;

            //Prep to throw the object!
            Vector2 finalVector = reletiveVector.normalized + rb.position; // Correct direction combined with the right start point (us!)
            heldObject.transform.SetParent(null);
            heldObject.transform.position = finalVector;
            //test.transform.position = reletiveVector.normalized + rb.position;
            Rigidbody2D objRb = heldObject.GetComponent<Rigidbody2D>();
            objRb.simulated = true;
            heldObject.GetComponent<BoxCollider2D>().enabled = true;
            
            //Throw the object!
            objRb.velocity = reletiveVector * yeetStrength;
            throwable = false;
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

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Item" && Input.GetButton("PickupThrow") && !holdingSomething)
        {
            holdingSomething = true;
            other.gameObject.transform.SetParent(holdSpot);
            heldObject = other.gameObject;
            heldObject.transform.position = holdSpot.position;
            heldObject.transform.rotation = holdSpot.rotation;
            heldObject.GetComponent<Rigidbody2D>().simulated = false;
            heldObject.GetComponent<BoxCollider2D>().enabled = false;
            throwable = false;
        }
    }
}
