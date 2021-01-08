using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Character_Controller : MonoBehaviour
{
    public Rigidbody rb;
    public Animator Player_Animator;
    public GameObject Player;
    public GameObject Sword;
    public GameObject Weapon_Spawn;
    public Quaternion Left;
    public Quaternion Right;
    public Quaternion Forward;
    public Quaternion Back;
    public float Speed;
    public float xPosition;
    public float zPosition;
    public bool Player_Has_Collided_With_Wall;
    public bool Jumping;
    public bool walking;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        xPosition = rb.transform.position.x;
        zPosition = rb.transform.position.z;
        Speed = 0.8f;

        Move();

        if (Jumping == true)
        {
            var Move = new Vector3(Input.GetAxis("Horizontal"), 1.9f, Input.GetAxis("Vertical"));
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

        if (Input.GetKeyDown("w") && Jumping == false|| Input.GetKeyDown("a") && Jumping == false|| Input.GetKeyDown("s") && Jumping == false || Input.GetKeyDown("d") && Jumping == false || Input.GetKeyDown("w") && Input.GetKeyDown("a") && Jumping == false || Input.GetKeyDown("w") && Input.GetKeyDown("d") && Jumping == false || Input.GetKeyDown("d") && Input.GetKeyDown("s") && Jumping == false || Input.GetKeyDown("s") && Input.GetKeyDown("a") && Jumping == false)
        {
            Player_Animator.Play("Walking");
        }

        if (!Input.anyKey && Jumping == false)
        {
            Player_Animator.Play("Idle");
        }

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

        if (Input.GetKeyDown("space") && Jumping == false)
        {
            Jumping = true;
            rb.velocity = new Vector3(0, 1f, 0);
            Player_Animator.Play("Jump");
            Player_Animator.Play("Jump", 0, 0);
        }

        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        //{
        //    if (hit.collider.tag == "Weapon")
        //    {
        //        Debug.Log("I found the sword");
        //        Sword.transform.position = Weapon_Spawn.transform.position;
        //        //parent the sword to the weapon spawn

        //    }
        //}

    }

    private void Move()
    {
        var Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += Move * Speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision theCollision)
    {
        //Check the player to see if they are colliding with the floor.
        if (theCollision.gameObject.tag == "Floor")
        {
            Jumping = false;
        }
    }
}
