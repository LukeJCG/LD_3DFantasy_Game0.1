﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Camera_Controller : MonoBehaviour
{
    public Transform Player_Transform;

    void Update()
    {
        transform.LookAt(Player_Transform);
    }
}
