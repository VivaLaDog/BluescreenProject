using UnityEngine;

public abstract class Interactable : MonoBehaviour
{    
    internal GameManager gm;
    internal Animator Animator;

    public bool interactedWith = false;

    [SerializeField] public string id;

    [ContextMenu("Generate GUID for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    internal void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Animator = GetComponent<Animator>();
    }

    public abstract void Interact();

    public virtual void ForceInteract()
    {
        interactedWith = true;
        Start();
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