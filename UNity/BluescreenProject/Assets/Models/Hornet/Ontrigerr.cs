using UnityEngine;

public class Ontrigerr : MonoBehaviour
{
    [SerializeField]
    AudioSource m_AudioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BLPlayerMovement>() != null)
        {
            m_AudioSource.Play();
            m_AudioSource.Pause();
            gameObject.SetActive(false);
        }
    }
}
