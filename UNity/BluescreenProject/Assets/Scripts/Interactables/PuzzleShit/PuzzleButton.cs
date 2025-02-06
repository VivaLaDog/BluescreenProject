using UnityEngine;

public class PuzzleButton : Interactable
{
    [SerializeField] GameObject disableThis;
    //Animator animator;
    public override void Interact()
    {
        /*animator = GetComponent<Animator>();
        animator.SetBool("Pressed", true);*/
        //literally kills an entity (disables it)
        if(disableThis != null)
        disableThis.SetActive(false);
    }
}
