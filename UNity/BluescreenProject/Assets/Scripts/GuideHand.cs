using UnityEngine;

public class GuideHand : MonoBehaviour
{
    Transform bl;
    Animator animator;
    bool turnOff = false;
    [SerializeField] GameObject optionalTurnOn;
    [SerializeField] GameObject previousOptionalTurnOn;
    void Start()
    {
        if(optionalTurnOn.activeSelf && optionalTurnOn != null)
        optionalTurnOn.SetActive(false);
        bl = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, bl.position) < 5)
        {
            animator.SetBool("param", true);
            if(optionalTurnOn != null)
                optionalTurnOn.SetActive(true);
            turnOff = true;
        }
        else
        {
            animator.SetBool("param", false);
            if (turnOff)
            {
                if(previousOptionalTurnOn != null)
                previousOptionalTurnOn.SetActive(false);
                turnOff = false;
            }
        }
    }
}
