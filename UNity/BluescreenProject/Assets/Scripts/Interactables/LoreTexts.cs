using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreTexts : Interactable
{
    [SerializeField] int loreDumpType = 0;
    [SerializeField] string[] text; //0 = headline, 1 = main text

    public override void Interact()
    {
        DumpLore();
    }
    private void DumpLore() //throw up a splashscreen of text
    {
        //maybe make it read from a file?
        if (loreDumpType == 0)
        {
            gm.ShowLore(text[0], text[1], loreDumpType);
        }
    }
}
