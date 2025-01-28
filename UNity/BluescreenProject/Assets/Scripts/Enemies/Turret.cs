using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : Enemy
{
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance;
    [SerializeField] float attackDistance;

    int health = 999;
    float time;

    protected override void Damage()
    {
        health--;
        if(health <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }

    internal void DamageMe()
    {
        Damage();
    }

    // Update is called once per frame
    void Update()
    {
        var c = Vector3.Distance(transform.position, target.position);

        //make raycast, if it can see the player, shoot

        if (c < maxViewDistance && Physics.Raycast(transform.position, target.position))
        {
            transform.LookAt(target);
            //Debug.Log(transform.name + " sees you!");

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
