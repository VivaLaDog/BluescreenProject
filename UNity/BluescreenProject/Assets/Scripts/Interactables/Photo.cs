using UnityEngine;
using UnityEngine.UI;

public class Photo : Interactable
{
    [SerializeField] Animator objToAnimate;
    [SerializeField] Image picture;
    public override void Interact()
    {
        if (picture.gameObject.activeSelf)
        {
            picture.gameObject.SetActive(false);
            return;
        }
        objToAnimate.SetBool("param", true);
        picture.gameObject.SetActive(true);
    }
    public override void ForceInteract()
    {
        base.ForceInteract();
        Interact();
    }
}
