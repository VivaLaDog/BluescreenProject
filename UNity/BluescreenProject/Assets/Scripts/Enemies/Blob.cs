using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Blob : Enemy
{
    [SerializeField] float speed = 1.5f;
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance = 6;
    [SerializeField] float attackDistance = 2f;
    int health = 10;
    float x = 0f;

    Vector3 ogPos;

    Animator animator;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ogPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
            DistanceCheck();
    }

    private void DistanceCheck()
    {
        //checks distance between og position and player, because i want this fella to only guard an area
        var c = Vector3.Distance(ogPos, target.position);
        
        //checks distance between player and blob, if within viewdistance and not obscured
        //if the player is within attack distance, it will attack
        if (c < maxViewDistance && Physics.Raycast(transform.position, target.position))
        {
            agent.SetDestination(target.position);

            if (c < attackDistance)
            {
                transform.LookAt(target);
                agent.isStopped = true;
                x += Time.deltaTime * 2 * speed;
                //BEGIN the ATTACK!
            }
            else
            {
                x -= Time.deltaTime * 2 * speed;
                agent.isStopped = false;
            }
        }
        else
        {
            agent.SetDestination(ogPos);
        }
        if (x < 0f) x = 0;
        if (x > 1f) x = 1f;

        animator.SetFloat("IsAttacking", x);


    }

    protected override void Die()
    {
        agent.enabled = false;
        ogPos = transform.position;
    }
    public void DamageMe()
    {
        Damage();
    }
    protected override void Damage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }
}
