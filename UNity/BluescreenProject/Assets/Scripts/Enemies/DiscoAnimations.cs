using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DiscoAnimations : Enemy
{
    [SerializeField] float speed = 1.5f;
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance = 20;
    [SerializeField] float attackDistance = 2;
    int health = 3;
        float x = 0f;
        float y = 0f;

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
        if(health > 0)
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        var c = Vector3.Distance(ogPos, target.position);
        //make raycast, if it can see the player, RELEASE THE DISCOMAN

        if (c < maxViewDistance && Physics.Raycast(transform.position, target.position))
        {
            y += Time.deltaTime * speed;
            //transform.localPosition = transform.forward * speed * Time.deltaTime;
            //Debug.Log(transform.name + " sees you!");
            agent.SetDestination(target.position);

            if (c < attackDistance)
            {
                transform.LookAt(target);
                agent.isStopped = true;
                x += Time.deltaTime * speed;
                //BEGIN the ATTACK!
            }
            else
            {
                x -= Time.deltaTime * speed;
                agent.isStopped = false;
            }
        }
        else
        {
            agent.SetDestination(ogPos);
            y -= Time.deltaTime * speed;
        }
        if (x < 0f) x = 0;
        if (x > 1f) x = 1f;
        if (y < 0f) y = 0;
        if (y > 1f) y = 1f;

        animator.SetFloat("IsAttacking", x);
        animator.SetFloat("IsHunting", y);


    }

    protected override void Die()
    {
        animator.SetFloat("IsDead", 1);
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
