using UnityEngine;
using UnityEngine.SceneManagement;

public class BigDoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BLPlayerMovement>() != null)
        {
            SceneManager.LoadScene(2);
        }
    }
}
