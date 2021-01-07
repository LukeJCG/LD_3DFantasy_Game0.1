﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Character_Controller : MonoBehaviour
{
    public Rigidbody rb;
    public Animator Player_Animator;
    public GameObject Player;
    public Quaternion Left;
    public Quaternion Right;
    public Quaternion Forward;
    public Quaternion Back;
    public float Speed;
    public float xPosition;
    public float zPosition;
    public bool Player_Has_Collided_With_Wall;
    public bool Is_Grounded;
    public bool Jumping;
    public bool walking;

    private void Start()
    {
        Is_Grounded = true;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        xPosition = rb.transform.position.x;
        zPosition = rb.transform.position.z;
        Speed = 0.5f;

        Move();

        if (Jumping == true)
        {
            var Move = new Vector3(Input.GetAxis("Horizontal"), 2, Input.GetAxis("Vertical"));
            transform.position += Move * Speed * Time.deltaTime;
        }

        if (zPosition == rb.transform.position.z)
        {
            walking = false;
        }

        if (xPosition == rb.transform.position.x)
        {
            walking = false;
        }
    }

    private void Update()
    {
        Left = Quaternion.Euler(0f, -90f, 0f);
        Right = Quaternion.Euler(0f, 90f, 0f);
        Forward = Quaternion.Euler(0f, -180f, 0f);
        Back = Quaternion.Euler(0f, 0f, 0f);

        Player_Has_Collided_With_Wall = Player.GetComponent<Player_Collision>().Player_Has_Collided_With_Wall;

        Jump();
        Player_Jump_Rotation();

        if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
        {
            Player_Animator.Play("Walking");
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
        {
            Player_Animator.Play("Idle");
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown("space") && Is_Grounded == true)
        {
            Jumping = true;
            rb.velocity = new Vector3(0, 0.5f, 0);
            Player_Animator.Play("Jump");
            var Move = new Vector3(Input.GetAxis("Horizontal"), 2, Input.GetAxis("Vertical"));
            transform.position += Move * Speed * Time.deltaTime;
        }
    }

    private void Move()
    {
        var Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += Move * Speed * Time.deltaTime;
    }

    private void Player_Jump_Rotation()
    {

        if (xPosition > rb.transform.position.x && Player_Has_Collided_With_Wall == false && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Left, Speed);
            walking = true;
        }

        if (xPosition < rb.transform.position.x && Player_Has_Collided_With_Wall == false && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Right, Speed);
            walking = true;
        }

        if (zPosition > rb.transform.position.z && Player_Has_Collided_With_Wall == false && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Forward, Speed);
            walking = true;
        }

        if (zPosition < rb.transform.position.z && Player_Has_Collided_With_Wall == false && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Back, Speed);
            walking = true;
        }
    }

    void OnCollisionEnter(Collision theCollision)
    {
        //Check the player to see if they are colliding with the floor.
        if (theCollision.gameObject.tag == "Floor")
        {
            Is_Grounded = true;
            Jumping = false;
        }
    }

    void OnCollisionExit(Collision theCollision)
    {
        //Check the player to see if they aren't colliding with the floor.
        if (theCollision.gameObject.tag == "Floor")
        {
            Is_Grounded = false;
        }
    }
}
