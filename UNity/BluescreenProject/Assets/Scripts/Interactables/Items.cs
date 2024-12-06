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
    }
    
    private void PickUpItem()
    {
        gm.Interaction(GetComponent<Items>());
        gameObject.SetActive(false);
    }
}