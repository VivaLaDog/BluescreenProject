using UnityEngine;

public abstract class Interactable : MonoBehaviour
{    
    internal GameManager gm;
    internal Animator Animator;

    internal void Start()
    {
        gm = transform.root.gameObject.GetComponent<GameManager>();
        Animator = GetComponent<Animator>();
    }

    public abstract void Interact();

    public virtual void ForceInteract()
    {

    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void DeactivateCanvas()
    {
        GetComponentInChildren<Canvas>().gameObject.SetActive(false);
    }
}