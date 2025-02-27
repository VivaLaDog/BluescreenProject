using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoreTexts : Interactable
{
    [SerializeField] string Header; //0 = headline, 1 = main text
    [SerializeField] string Body;
    [SerializeField] GameObject canvas;
    bool reading = false;
    
    public override void Interact()
    {
        DumpLore();
    }
    private void DumpLore() //throw up a splashscreen of text
    {
        //maybe make it read from a file?
        
        if(reading == false)
        {
            ShowLore(Header, Body);
            reading = true;
        }
        else
        {
            HideLore();
            reading = false;
        }
    }
    void HideLore()
    {
        gm.ChangeBLMovement();
        var ts = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        canvas.gameObject.SetActive(false);
        ts[0].text = "";
        ts[1].text = "";
    }
    internal void ShowLore(string text, string info)
    {
        gm.ChangeBLMovement();
        var ts = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        canvas.gameObject.SetActive(true);
        if (text != null)
            ts[0].text = text;
        if (info != "")
            ts[1].text = info;
    }
}
/*
 * Subject #413 is showing signs of weariness and anxiety. Bring her some enrichment items, please. Thanks.
 * This door is not getting enough power, so I moved some power from the lever on the otherside. It barely opens the main door now so you actually do need to pull both levers.
 * Screw this job, we are supposed to inspect anomalous materials, but all we've been expecting these past few weeks has been some posters and that weird girl.
 */