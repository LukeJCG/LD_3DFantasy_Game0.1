using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float Rotation_Speed;
    public float xPosition;
    public float zPosition;
    public bool Player_Has_Collided_With_Wall;
    public bool Jumping;
    public bool walking;
    public Camera Camera;
    //public Text Action_Text;
    //public bool Found_Sword;
    //public bool Sword_Equipped;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Found_Sword = false;
    }

    private void FixedUpdate()
    {
        xPosition = rb.transform.position.x;
        zPosition = rb.transform.position.z;
        Speed = 0.8f;
        Rotation_Speed = 2.5f;

        if (Player_Has_Collided_With_Wall == false)
        {
            Move_Camera();
        }

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

        //Camera.transform.rotation = rb.transform.rotation;
    }

    private void Update()
    {
        Left = Quaternion.Euler(0f, -90f, 0f);
        Right = Quaternion.Euler(0f, 90f, 0f);
        Forward = Quaternion.Euler(0f, -180f, 0f);
        Back = Quaternion.Euler(0f, 0f, 0f);

        Camera.transform.Rotate(Player.transform.position, Speed * Time.deltaTime);
        Camera.transform.position = rb.transform.position + new Vector3(0, 2, -1);

        Player_Has_Collided_With_Wall = Player.GetComponent<Player_Collision>().Player_Has_Collided_With_Wall;

        if (Input.GetKey("w") && Jumping == false || Input.GetKey("a") && Jumping == false || Input.GetKey("s") && Jumping == false || Input.GetKey("d") && Jumping == false || Input.GetKey("w") && Input.GetKey("a") && Jumping == false || Input.GetKey("w") && Input.GetKey("d") && Jumping == false || Input.GetKey("d") && Input.GetKey("s") && Jumping == false || Input.GetKey("s") && Input.GetKey("a") && Jumping == false)
        {
            Player_Animator.Play("Walking");
        }

        if (!Input.anyKey && Jumping == false)
        {
            Player_Animator.Play("Idle");
        }

        if (xPosition > rb.transform.position.x && Player_Has_Collided_With_Wall == false && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Left, Rotation_Speed * Time.deltaTime);
            walking = true;
        }

        if (xPosition < rb.transform.position.x && Player_Has_Collided_With_Wall == false && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Right, Rotation_Speed * Time.deltaTime);
            walking = true;
        }

        if (zPosition > rb.transform.position.z && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Forward, Rotation_Speed * Time.deltaTime);
            walking = true;
        }

        if (zPosition < rb.transform.position.z && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Back, Rotation_Speed * Time.deltaTime);
            walking = true;
        }

        if (Input.GetKeyDown("space") && Jumping == false)
        {
            Jumping = true;
            rb.velocity += new Vector3(0, 1f, 0);
            //Camera.transform.position = new Vector3(0, 1f, 0);
            Player_Animator.Play("Jump");
            Player_Animator.Play("Jump", 0, 0);
        }

        //if (Found_Sword == false)
        //{
        //    Action_Text.text = "";
        //}

        //if (Found_Sword == true && Input.GetKeyDown("e"))
        //{
        //    Sword.transform.position = Weapon_Spawn.transform.position;
        //    Sword.transform.parent = Weapon_Spawn.transform;
        //}

        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        //{
        //    if (hit.collider.name == "Sword")
        //    {
        //    }

        //}
    }

    private void Move_Camera()
    {
        var Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Camera.transform.position += Move * Speed * Time.deltaTime;
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

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Weapon")
    //    {
    //        Debug.Log("Sword is here");
    //        Action_Text.text = "Press e to pickup the sword.";
    //        Found_Sword = true;
    //    }

    //}
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Weapon")
    //    {
    //        Found_Sword = false;
    //    }
    //}
}
