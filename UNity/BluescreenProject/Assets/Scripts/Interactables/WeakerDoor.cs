using UnityEngine;

public class WeakerDoor : Doors
{
    public override void Interact()
    {
        
    }
    
    new void OpenDoor()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("OpenDoor", true);
    }
}
