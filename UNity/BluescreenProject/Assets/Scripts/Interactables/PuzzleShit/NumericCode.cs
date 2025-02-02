using UnityEngine;
using UnityEngine.UIElements;

public class NumericCode : Interactable
{
    [SerializeField] Doors unlocksDoor;
    [SerializeField] int code;
    [SerializeField] Transform numpad;

    public override void Interact()
    {   /* upon interaction throw up a button screen overlay thing
         * player presses the buttons to match the code
         * check if inputted code = correct code
         * if bad, fuck off and error {with font size 24, and then back to 36}, if good unlock door
         */
       


    }
}