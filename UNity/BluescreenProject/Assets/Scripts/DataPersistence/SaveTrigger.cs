using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataPersistenceManager>().SaveGame();
            gameObject.SetActive(false);
        }
    }
}
