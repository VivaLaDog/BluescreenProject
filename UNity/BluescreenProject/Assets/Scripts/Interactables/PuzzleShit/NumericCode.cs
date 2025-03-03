using UnityEngine;
public class NumericCode : Interactable
{
    [SerializeField] Doors unlocksDoor;
    [SerializeField] int code;
    [SerializeField] NumButtPress numpad;
    bool doneInteract = false;
    public override void Interact()
    {
        if (doneInteract)
        {
            return;
        }

        doneInteract = true;
        gm.ChangeBLMovement();
        numpad.OpenPad();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gm.AddToInteractedList(this.GetComponent<Interactable>());
    }
    public override void ForceInteract()
    {
        base.ForceInteract();
        PassDoor();
        doneInteract = true;
        DeactivateCanvas();
    }
    internal void PassDoor()
    {
        unlocksDoor.UnlockDoor();
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