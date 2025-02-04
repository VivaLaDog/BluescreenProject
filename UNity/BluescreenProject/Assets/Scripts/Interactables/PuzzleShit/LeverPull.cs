using UnityEngine;

public class LeverPull : Interactable
{
    [SerializeField] Animator opens;
    [SerializeField] Animator opens2;
    [SerializeField] Camera camera2;
    [SerializeField] Camera camera1;
    [SerializeField] string parameter;
    [SerializeField] Doors doorToOpen;

    Animator animator;
    bool cameraSwapped = false;
    public override void Interact()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Pulled", true);
        opens.SetBool(parameter, true);
        if(opens2 != null)
        opens2.SetBool(parameter, true);
        if(camera2 != null)
        {
            camera2.gameObject.SetActive(true);
            camera1.gameObject.SetActive(false);
            cameraSwapped = true;
        }
        if (doorToOpen != null)
        {
            doorToOpen.UnlockDoor();
        }
    }
    float timer = 5;
    private void Update()
    {
        if (cameraSwapped)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                camera1.gameObject.SetActive(true);
                camera2.gameObject.SetActive(false);
                cameraSwapped = false;
            }
        }
    }
}
