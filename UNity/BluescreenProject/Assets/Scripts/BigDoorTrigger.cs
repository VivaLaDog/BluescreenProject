using UnityEngine;
using UnityEngine.SceneManagement;

public class BigDoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BLPlayerMovement>() != null)
        {
            DataPersistenceManager.Instance.NewGame();
            SceneManager.LoadSceneAsync(2);
        }
    }
}
