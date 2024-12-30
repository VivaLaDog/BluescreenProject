using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VivaAnimations : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("IsWalking");
        bool isRunning = animator.GetBool("IsRunning");
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        if (!isWalking && forwardPressed)
        {
            animator.SetBool("IsWalking", true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool("IsWalking", false);
        }

        if(!isRunning && (forwardPressed && shiftPressed))
        {
            animator.SetBool("IsRunning", true);
        }

        if (isRunning && (!forwardPressed || !shiftPressed))
        {
            animator.SetBool("IsRunning", false);
        }
    }
}
