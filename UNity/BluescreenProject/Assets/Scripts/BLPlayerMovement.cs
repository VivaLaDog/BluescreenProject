using System;
using UnityEngine;

public class BLPlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    bool reading = false;
    Vector3 cameraRotation;
    Vector3 rotation;
    [SerializeField] Transform cameraHolder;
    [SerializeField] float sensitivity;
    bool mouseState = true;

    [SerializeField]
    AudioSource walkingSfx;
    [SerializeField]
    AudioSource interactSfx;
    public bool IsMoving()
    {
        return reading;
    }

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    bool spawn = true;
    float spawnTimer = .98f;
    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer < 0)
            {
                spawn = false;
            }
        }
        else
        {
            if (!reading)
            {
                PlayerMove();
                if (mouseState)
                    Rotation();
            }
        }
    }

    private void PlayerMove()
    {
        float xM = Input.GetAxis("Horizontal");
        float zM = Input.GetAxis("Vertical");
        float yM = rb.linearVelocity.y;

        if (xM == 0 && zM == 0)
        {
            walkingSfx.Pause();
            return;
        }
        walkingSfx.UnPause();

        var dir = new Vector3(xM, 0, zM);
        var transformedDir = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * dir;
        rb.linearVelocity = new Vector3(transformedDir.x * speed, yM, transformedDir.z * speed);  
        rb.MoveRotation(Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y + (xM * (float)1.38), rb.rotation.eulerAngles.z));
    }

    internal void StopMoving(bool yes)
    {
        reading = yes;
        mouseState = !yes;
    }

    private void Rotation()
    {
        float xRot = Input.GetAxisRaw("Mouse X");
        float yRot = Input.GetAxisRaw("Mouse Y");

        cameraRotation.x += yRot * sensitivity;
        rotation.y += xRot * sensitivity;

        transform.rotation = Quaternion.Euler(rotation);

        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -58, 70);

        cameraHolder.localRotation = Quaternion.Euler(cameraRotation);
    }

    public void PlayInteractSound()
    {
        interactSfx.Play();
    }
    private AudioSource mainWalkingSfx;
    internal void SwapWalkSFX(AudioSource aud)
    {
        if(mainWalkingSfx == null)
        mainWalkingSfx = walkingSfx;
        walkingSfx.Pause();
        walkingSfx = aud;
        walkingSfx.UnPause();
    }

    internal void ResetWalkSFX()
    {
        walkingSfx.Pause();
        walkingSfx = mainWalkingSfx;
        walkingSfx.UnPause();
    }
}
