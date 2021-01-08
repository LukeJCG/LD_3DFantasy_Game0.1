using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behaviour : MonoBehaviour
{
    public Transform Player_Transform;

    void Update()
    {
        transform.LookAt(Player_Transform);
    }
}
