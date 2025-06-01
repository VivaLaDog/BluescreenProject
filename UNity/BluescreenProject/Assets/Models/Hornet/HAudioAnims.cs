using System.Threading;
using UnityEngine;

public class HAudioAnims : MonoBehaviour
{
    [SerializeField]
    HAnimScript Hornet;
    [SerializeField]
    int animToPlay;
    [SerializeField]
    Collider CollToTurnOff;
    [SerializeField]
    AudioSource AudioSource;

    float timer = 0;
    [SerializeField]
    float voiceLineLength = 0;
    bool timerOn = false;

    private void Start()
    {
        voiceLineLength = voiceLineLength * 1;
    }
    private void Update()
    {
        if (timerOn)
        {
            if (timer > voiceLineLength)
            {
                CollToTurnOff.enabled = false;
                timerOn = false;
                AudioSource.Pause();
                if (animToPlay == 4)
                    Hornet.gameObject.SetActive(false);
            }
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BLPlayerMovement>() != null && timerOn == false)
        {
            if (animToPlay == 1)
                Hornet.PlayAnim1();
            if (animToPlay == 2)
                Hornet.PlayAnim2();
            if (animToPlay == 3)
                Hornet.PlayAnim3();
            if (animToPlay == 4)
                Hornet.PlayAnim4();
            timerOn = true;
            AudioSource.UnPause();
        }
    }
}
