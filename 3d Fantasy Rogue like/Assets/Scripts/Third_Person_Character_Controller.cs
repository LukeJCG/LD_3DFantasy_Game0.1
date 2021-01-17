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
    public Camera Camera;
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
        Speed = 1.5f;
        Rotation_Speed = 3f;

        Move();

        if (Jumping == true)
        {
            var Move = new Vector3(Input.GetAxis("Horizontal"), 2.5f, Input.GetAxis("Vertical"));
            transform.position += Move * Speed * Time.deltaTime;
        }

        if (xPosition > rb.transform.position.x && Player_Has_Collided_With_Wall == false && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Left, Rotation_Speed * Time.deltaTime);
        }

        if (xPosition < rb.transform.position.x && Player_Has_Collided_With_Wall == false && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Right, Rotation_Speed * Time.deltaTime);
        }

        if (zPosition > rb.transform.position.z && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Forward, Rotation_Speed * Time.deltaTime);
        }

        if (zPosition < rb.transform.position.z && Jumping == false)
        {
            rb.transform.rotation = Quaternion.Slerp(transform.rotation, Back, Rotation_Speed * Time.deltaTime);
        }
    }

    private void Update()
    {
        Left = Quaternion.Euler(0f, -90f, 0f);
        Right = Quaternion.Euler(0f, 90f, 0f);
        Forward = Quaternion.Euler(0f, -180f, 0f);
        Back = Quaternion.Euler(0f, 0f, 0f);

        //Camera.transform.Rotate(rb.transform.position, Speed * Time.deltaTime);

        Player_Has_Collided_With_Wall = Player.GetComponent<Player_Collision>().Player_Has_Collided_With_Wall;

        if (Input.GetKey("w") && Jumping == false || Input.GetKey("a") && Jumping == false || Input.GetKey("s") && Jumping == false || Input.GetKey("d") && Jumping == false || Input.GetKey("w") && Input.GetKey("a") && Jumping == false || Input.GetKey("w") && Input.GetKey("d") && Jumping == false || Input.GetKey("d") && Input.GetKey("s") && Jumping == false || Input.GetKey("s") && Input.GetKey("a") && Jumping == false)
        {
            Player_Animator.Play("Walking");
        }

        if (!Input.anyKey && Jumping == false)
        {
            Player_Animator.Play("Idle");
        }

        if (Input.GetKeyDown("space"))
        {
            Jumping = true;
            rb.velocity += new Vector3(0, 1f, 0);
            Player_Animator.Play("Jump");
            Player_Animator.Play("Jump", 0, 0);
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(0, -0.3f, 0), out hit))
        {
            if (hit.collider.tag == "Floor")
            {
                Vector3 down = transform.TransformDirection(0, -0.3f, 0);
                Debug.DrawRay(transform.position, down, Color.red);
                Jumping = false;
            }
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

    private void Move()
    {
        var Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += Move * Speed * Time.deltaTime;
        Camera.transform.position = rb.transform.position + new Vector3(0, 4, -2);
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
