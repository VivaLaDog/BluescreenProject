using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableHighlights : MonoBehaviour
{
    List<Interactable> interactables = new List<Interactable>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Interactable[] gwah = GetComponentsInChildren<Interactable>();//two lists, one for interactables and one for doors, doors get larger light area
        
        for(int i = 0;  i < gwah.Length; i++)
        {
            //Debug.Log(gwah[i].gameObject.name);
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
                AddLightComp(interactable, 3f, LightShadows.Soft, 0.5f);
            }
            
            interactable.AddComponent<HighLightChanger>();
        }

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
