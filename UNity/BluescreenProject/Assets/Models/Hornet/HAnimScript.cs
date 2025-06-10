using UnityEngine;

public class HAnimScript : MonoBehaviour
{
    Animator Hanim;
    [SerializeField]
    Animator Hanim2;

    void Start()
    {
        Hanim = GetComponent<Animator>();
    }

    public void PlayAnim1()
    {
        gameObject.SetActive(true);
    }
    public void PlayAnim2()
    {
        Hanim.SetBool("Cond1", true);
    }
    public void PlayAnim3()
    {
        Hanim.SetBool("Cond2", true);
        Hanim2.SetBool("Cond1", true);
    }
    public void PlayAnim4()
    {
        Hanim.SetBool("Cond3", true);
        Hanim2.SetBool("Cond1", false);
    }
}