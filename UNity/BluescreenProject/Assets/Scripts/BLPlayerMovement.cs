using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class BLPlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    bool reading = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!reading)
        PlayerMove();
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StopMoving(false);
            }
        }
    }

    private void PlayerMove()
    {
        float xM = Input.GetAxis("Horizontal");
        float zM = Input.GetAxis("Vertical");
        float yM = rb.velocity.y;

        var dir = new Vector3(0, 0, zM);
        var transformedDir = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * dir;

        if (Input.GetKeyDown("left shift"))
            speed = speed * 2;
        else if (Input.GetKeyUp("left shift"))
            speed = speed / 2;

        rb.velocity = new Vector3(transformedDir.x * speed, yM, transformedDir.z * speed);
        rb.MoveRotation(Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y + (xM * (float)1.38), rb.rotation.eulerAngles.z));
    }

    internal void StopMoving(bool yeah)
    {
        reading = yeah;
    }
}
