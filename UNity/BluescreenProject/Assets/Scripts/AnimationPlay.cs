using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationPlay : MonoBehaviour
{
    private Animator mAnimator;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(mAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                mAnimator.SetTrigger("Open");
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                mAnimator.SetTrigger("Close");
            }
        }
    }
}
