using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] bool pickUp;
    [SerializeField] bool door;
    [SerializeField] bool locked;
    [SerializeField] GameObject doorKey;
    [SerializeField] int loreDumpType = 0;
    [SerializeField] string[] text;

    GameManager gm;
    GameObject me;
    private Animator mAnimator;

    public void Interact()
    {
        mAnimator = GetComponent<Animator>();
        me = this.gameObject;
        if (!me.activeSelf)
        {
            Debug.Log("Already been picked up!");
            //gm.ForwCheck().gameObjectsInArea.Clear();
            return;
        }
        gm = transform.root.gameObject.GetComponent<GameManager>();
        Debug.Log($"{pickUp} / {door} / {locked} / {doorKey}");
        if (pickUp)
        {
            PickUpItem();
        }
        else if (door)
        {
            if (locked)
            {
                if (gm.GetPickedItems().Count < 1)
                {
                    Debug.Log("oopsie no items");
                }

                for(int i = 0; i < gm.GetPickedItems().Count; i++)
                {
                    TryOpenDoorLocked(gm.GetPickedItems()[i]);
                }
            }
            else
            {
                OpenDoor();
            }
        }
        else //loredump
        {
            DumpLore();
        }
    }

    private void TryOpenDoorLocked(GameObject key) //check if player has required keycard
    {
        if(doorKey == key)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("FUCK YOU");
        }
    }

    private void DumpLore() //throw up a splashscreen of text
    {//maybe make it read from a file?
        Debug.Log("lore");
        gm.ShowLore(text[0], text[1], loreDumpType);
    }
    private void OpenDoor() // is working, add animation nowe
    {
        Debug.Log("DOOR OPENED");
        //transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
        mAnimator.SetTrigger("Open");
        //gm.BLTransition(GetComponent<Interactable>());
    }

    public void CloseDoor()
    {
        mAnimator.SetTrigger("Close");
    }

    private void PickUpItem()
    {
        Debug.Log(me);
        gm.Interaction(me);
        me.SetActive(false);
    }


}