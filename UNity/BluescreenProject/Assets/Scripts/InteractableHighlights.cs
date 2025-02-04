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
            Debug.Log(gwah[i].gameObject.name);
            interactables.Add(gwah[i]);
        }

        foreach(Interactable interactable in interactables)
        {
            if(interactable.GetComponent<Doors>() != null)//gives no light, which... fair because its a door, you'll know you can access it by the bottom light
            {
                //this code doesnt exist
                Doors door = interactable.GetComponent<Doors>();
                door.AddComponent<Light>();
                Light intLight = door.GetComponent<Light>();
                intLight.intensity = .8f;
                intLight.shadows = LightShadows.Soft;
                intLight.shadowRadius = 1f;
                intLight.range = 5f;
            }
            else
            {
                interactable.AddComponent<Light>();
                Light intLight = interactable.GetComponent<Light>();
                intLight.intensity = 3f;
                intLight.shadows = LightShadows.Soft;
                intLight.shadowRadius = 0.4f;
                intLight.range = 0.5f;
            }
        }

    }
}
