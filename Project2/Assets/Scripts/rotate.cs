﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{   
    public float speed = 5.0f;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
