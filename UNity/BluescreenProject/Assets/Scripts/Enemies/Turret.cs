using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance = 20;
    [SerializeField] float attackDistance = 8;

    float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float a = transform.position.x - target.position.x;
        float b = transform.position.z - target.position.z;
        float c = Mathf.Sqrt(a * a + b * b);

        //make raycast, if it can see the player, shoot

        if (c < maxViewDistance && Physics.Raycast(transform.position, target.position))
        {
            transform.LookAt(target);
            Debug.Log(transform.name + " sees you!");

            if (c < attackDistance)
            {
                //BEGIN the ATTACK!
                //count to 3, shoot (take health from player) repeat
                if(time == 3000)
                {
                    Debug.Log("SHOOT");
                }
            }
        }
    }
}
