using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    public bool Player_Has_Collided_With_Wall;

    void OnTriggerEnter(Collider other)
    {
        //Check the player to see if they are colliding with the wall infront of them.
        if (other.gameObject.tag == "Is_Trigger_Collider")
        {
            Player_Has_Collided_With_Wall = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Check the player to see if they are not are colliding with the wall infront of them.
        if (other.gameObject.tag == "Is_Trigger_Collider")
        {
            Player_Has_Collided_With_Wall = false;
        }
    }
}
