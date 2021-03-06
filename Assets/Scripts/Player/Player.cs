﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 5;

    [SerializeField]
    GameObject Ball;

    Rigidbody2D rb;

    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //this.transform.position +=
            //new Vector3(-speed, 0);

            rb.velocity =
                new Vector2(-speed, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //this.transform.position +=
            //new Vector3(speed, 0);

            rb.velocity =
                new Vector2(speed, 0);
        }
        

        if (!isStart &&
            (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            Ball.GetComponent<BaseBall>().enabled = true;

            isStart = true;
        }
    }
}
