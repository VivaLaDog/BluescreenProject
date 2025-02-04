using UnityEngine;

public class Poster : Interactable
{
    [SerializeField] GameObject panel;
    public override void Interact()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
            //poster jumpscare
            gm.ChangeBLMovement();
        }
        else
        {
            gm.ChangeBLMovement();
            panel.SetActive(false);
        }
    }
    private void Update()
    {
        if(Vector3.Distance(gm.BLPos(), transform.position) > 3)
        {
            panel.SetActive(false);
        }
    }
}
