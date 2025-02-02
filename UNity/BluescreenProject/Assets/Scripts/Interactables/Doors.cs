using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doors : Interactable
{
    [SerializeField] bool locked;
    [SerializeField] Items doorKey;
    [SerializeField] Doors travelTo;

    public override void Interact()
    {
        if (locked)
        {
            if (gm.GetPickedItems().Count < 1)
            {
                Debug.Log("oopsie no items");
                return;
            }
            for (int i = 0; i < gm.GetPickedItems().Count; i++)
            {
                TryOpenDoorLocked(gm.GetPickedItems()[i]);
            }
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor() // is working, add animation nowe
    {
        //transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
        //transform.parent.GetComponent<Animator>().SetTrigger("Open");
        //Animator.SetTrigger("Open");
        gm.BLTransition(GetComponent<Doors>(), travelTo);
    }
    public void UnlockDoor()
    {
        locked = false;
        //maybe change door colour?
    }
    private void TryOpenDoorLocked(Items key) //check if player has required keycard
    {
        if (doorKey == key)
        {
            UnlockDoor();
        }
        else
        {
            Debug.Log("FUCK YOU");
        }
    }

}
