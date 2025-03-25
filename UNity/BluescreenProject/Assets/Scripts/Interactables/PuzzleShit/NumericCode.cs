using UnityEngine;
public class NumericCode : Interactable
{
    [SerializeField] Doors unlocksDoor;
    [SerializeField] int code;
    [SerializeField] NumButtPress numpad;
    public override void Interact()
    {
        if (interactedWith)
            return;

        gm.ChangeBLMovement();
        numpad.OpenPad();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gm.AddToInteractedList(this.GetComponent<Interactable>());
        interactedWith = false;
    }
    public override void ForceInteract()
    {
        base.ForceInteract();
        PassDoor();
    }
    internal void PassDoor()
    {
        interactedWith = true;
        unlocksDoor.UnlockDoor();
        DeactivateCanvas();
    }

    internal bool CheckCode(int checkCode)
    {
        if(checkCode == code) return true;
        else
        return false;
    }

    internal void CauseMayhemInCode()
    {
        gm.ChangeBLMovement();
    }
}