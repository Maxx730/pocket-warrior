﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGroup : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(Mathf.Sin(Time.time), transform.position.y, transform.position.z);       
    }
}
