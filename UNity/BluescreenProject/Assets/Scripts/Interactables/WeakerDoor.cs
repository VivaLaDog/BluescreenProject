using UnityEngine;

public class WeakerDoor : Interactable
{
new private void Start()
    {
        base.Start();
        DeactivateCanvas();
    }
    public override void Interact() { }
    
    public void OpenDoor()
    {
        Animator = GetComponent<Animator>();
        Animator.SetBool("OpenDoor", true);
    }
    public override void ForceInteract()
    {
        base.ForceInteract();
        OpenDoor();
    }
}
