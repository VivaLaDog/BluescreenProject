using UnityEngine;
public class NumericCode : Interactable
{
    [SerializeField] Doors unlocksDoor;
    [SerializeField] int code;
    [SerializeField] NumButtPress numpad;

    public override void Interact()
    {   /* upon interaction throw up a button screen overlay thing
         * player presses the buttons to match the code
         * check if inputted code = correct code
         * if bad, fuck off and error {with font size 24, and then back to 36}, if good unlock door
         */

        gm.ChangeBLMovement();
        numpad.OpenPad();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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