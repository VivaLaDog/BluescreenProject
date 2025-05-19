using UnityEngine;

public class PhotoChecker : MonoBehaviour
{
    Animator animator;
    Photo photo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        photo = GetComponentInChildren<Photo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photo.gameObject.activeSelf)
        {
            animator.SetBool("param", false);
            animator.SetBool("param2", true);
        }
    }
}
