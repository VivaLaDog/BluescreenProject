using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private bool triggerEnabled = false;
    private float timer = 1f;
    private void Update()
    {
        if (!triggerEnabled)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                triggerEnabled = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && triggerEnabled)
        {
            GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataPersistenceManager>().SaveGame();
            gameObject.SetActive(false);
        }
    }
}
