using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoAnimations : MonoBehaviour
{
    [SerializeField] float speed = 1.2f;
    [SerializeField] Transform target;
    [SerializeField] float maxViewDistance = 10;
    [SerializeField] float attackDistance = 2;
    [SerializeField] int health = 5;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }


        float a = transform.position.x - target.position.x;
        float b = transform.position.z - target.position.z;
        float c = Mathf.Sqrt(a * a + b * b);

        //make raycast, if it can see the player, RELEASE THE DISCOMAN
        
        if(c < maxViewDistance)
        {
            animator.SetBool("IsHunting", true);
            transform.LookAt(target);
            transform.localPosition = transform.forward * speed * Time.deltaTime;
                Debug.Log(transform.name + " sees you!");

            if (c < attackDistance)
            {
                //BEGIN the ATTACK!
            }
        }

        
        animator.SetBool("IsHunting", false);
    }

    private void Die()
    {
        animator.SetBool("IsDead", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //make target take damage...
    }
}
