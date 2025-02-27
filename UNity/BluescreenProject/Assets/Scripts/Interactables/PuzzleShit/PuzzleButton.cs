using UnityEngine;

public class PuzzleButton : Interactable
{
    [SerializeField] GameObject disableThis;
    Animator animator;

    bool pressed = false;
    public override void Interact()
    {
        if(pressed == true)
            return;
        
        animator = GetComponent<Animator>();
        animator.SetBool("Pressed", true);
        //literally kills an entity (disables it)
        if(disableThis != null)
        disableThis.SetActive(false);

        DeactivateCanvas();
        pressed = true;
        gm.AddToInteractedList(this.GetComponent<Interactable>());
    }
    public override void ForceInteract()
    {
        pressed = true;
        disableThis.SetActive(false);
        DeactivateCanvas();
    }
}
