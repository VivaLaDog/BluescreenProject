using UnityEngine;

public class NumericCode2 : Interactable
{
    [SerializeField] WeakerDoor doorToAnimate;
    [SerializeField] int code;
    [SerializeField] NumButtPress2 numpad;
    [SerializeField] bool confirm;

new private void Start()
    {
        base.Start();
        numpad.confirm = confirm;
    }
    public override void Interact()
    {
        if (interactedWith)
            return;

        gm.ChangeBLMovement();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (numpad.gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            numpad.gameObject.SetActive(false);
            return;
        }

        numpad.OpenPad();
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
        doorToAnimate.OpenDoor();
        DeactivateCanvas();
    }

    public int GetCode()
    {
        return code;
    }

    internal bool CheckCode(int checkCode)
    {
        if (checkCode == code) return true;
        else
            return false;
    }

    internal void CauseMayhemInCode()
    {
        gm.ChangeBLMovement();
    }
}
