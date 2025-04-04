using UnityEngine;

public class PadlockedDoor : MonoBehaviour
{
    protected int unlockedLocks;
    public int neededUnlocks;

    private void Update()
    {
        if(neededUnlocks == unlockedLocks)
        {
            OpenMe();
        }
    }

    private void OpenMe()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Open", true);
    }

    public void AddUnlocks()
    {
        unlockedLocks++;
    }
}
