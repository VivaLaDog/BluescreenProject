using System;
using UnityEngine;

public class ChangeWalkingSfx : MonoBehaviour
{
    [SerializeField] AudioSource newWalkSfx;

    BLPlayerMovement guh;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BLPlayerMovement>() != null)
        {
            guh = collision.gameObject.GetComponent<BLPlayerMovement>();
            ChangeSFX();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<BLPlayerMovement>() != null)
        {
            guh = collision.gameObject.GetComponent<BLPlayerMovement>();
            ChangeSFX2();
        }
    }

    private void ChangeSFX2()
    {
        guh.ResetWalkSFX();
    }

    private void ChangeSFX()
    {
        guh.SwapWalkSFX(newWalkSfx);
    }
}
