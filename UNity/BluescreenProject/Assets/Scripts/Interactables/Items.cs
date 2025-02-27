using Unity.VisualScripting;
using UnityEngine;

public class Items : Interactable
{
    public override void Interact()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        PickUpItem();
        gm.AddToInteractedList(this);
        DeactivateCanvas();
    }
    
    private void PickUpItem()
    {
        gm.Interaction(GetComponent<Items>());
        gameObject.transform.position = new Vector3(0, 10, 0);
        gameObject.SetActive(false);
    }
}