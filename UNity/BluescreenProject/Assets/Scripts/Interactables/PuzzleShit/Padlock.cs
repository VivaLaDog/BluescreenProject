using System.Collections.Generic;
using UnityEngine;

public class Padlock : Interactable
{
    [SerializeField]
    List<PadlockedDoor> doorsToOpen;

    [SerializeField]
    Items key;
    public override void Interact()
    {
        if (gm.pickedUpItems.Count == 0 || !gm.pickedUpItems.Contains(key) || !gameObject.activeSelf)
            return;
        doorsToOpen.ForEach(i => i.AddUnlocks());
        gm.AddToInteractedList(this);
        Deactivate();
        DeactivateCanvas();
        GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataPersistenceManager>().SaveGame();
    }

    public override void ForceInteract()
    {
        base.ForceInteract();
    }
}
