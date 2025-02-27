using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableHighlights : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    List<Interactable> interactables = new List<Interactable>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Interactable[] gwah = GetComponentsInChildren<Interactable>();
        
        for(int i = 0;  i < gwah.Length; i++)
        {
            interactables.Add(gwah[i]);
        }

        foreach(Interactable interactable in interactables)
        {
            if (interactable.GetComponent<Doors>() != null)
            {
                Doors door = interactable.GetComponent<Doors>();
                AddLightComp(door, .8f, LightShadows.Soft, 5f);
            }
            else
            {
                if(interactable.GetComponent<Light>() == null)
                AddLightComp(interactable, 2f, LightShadows.Soft, 0.5f);
            }
            
            AddInteractHighlight(interactable);
            interactable.AddComponent<HighLightChanger>();
        }

    }

    private void AddInteractHighlight(Interactable interactable)
    {
        //the highlight is always hidden partially by objects, find a way to fix that. :)
        var intCanvas = Instantiate(canvas, interactable.transform);
        intCanvas.AddComponent<IntImageScale>();
    }

    private static void AddLightComp(Interactable interactable, float intensity, LightShadows shadow, float range)
    {
        interactable.AddComponent<Light>();
        Light intLight = interactable.GetComponent<Light>();
        intLight.intensity = intensity;
        intLight.shadows = shadow;
        intLight.range = range;
    }
}
