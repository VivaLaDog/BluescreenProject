using UnityEngine;
using UnityEngine.SceneManagement;

public class BigDoorTrigger : MonoBehaviour
{
    [SerializeField] int sceneInt = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BLPlayerMovement>() != null)
        {
            DataPersistenceManager.Instance.ClearGameData();
            if(sceneInt == 2)
            other.gameObject.transform.position = new Vector3(0, 1.02f, -58f);
            else if(sceneInt == 3)
            other.gameObject.transform.position = new Vector3(0, 1.02f, 11f);
            DataPersistenceManager.Instance.SaveGame();
            SceneManager.LoadScene(sceneInt);
        }
    }
}
