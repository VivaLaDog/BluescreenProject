using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] Button butt1;
    [SerializeField] Button butt2;
    [SerializeField] Button butt3;
    public void StartNewGame() 
    {
        DisableButtons();
        DataPersistenceManager.Instance.NewGame();
        SceneManager.LoadSceneAsync(1);
    }
    private void Start()
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            butt1.GetComponent<Image>().gameObject.SetActive(false);
        }
    }
    public void ContinueGame()
    {
        DisableButtons();
        DataPersistenceManager.Instance.ContinueGame();

    }
    public void ExitGame()
    {
        DisableButtons();
        Application.Quit();
    }

    private void DisableButtons()
    {
        butt1.interactable = false;
        butt2.interactable = false;
        butt3.interactable = false;
    }
}
