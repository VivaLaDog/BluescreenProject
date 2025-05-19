using UnityEngine;

public class FacButton : Interactable
{
    [SerializeField] Animator objToAnimate;
    public override void Interact()
    {
        GetComponent<Animator>().SetBool("Pressed", true);
        objToAnimate.SetBool("param", true);
        gm.AddToInteractedList(this.GetComponent<Interactable>());
    }

    public override void ForceInteract()
    {
        base.ForceInteract();
        GetComponent<Animator>().SetBool("Pressed", true);
        objToAnimate.SetBool("param", true);
    }
}