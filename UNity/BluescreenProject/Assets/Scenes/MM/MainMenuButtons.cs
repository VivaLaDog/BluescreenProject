using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartNewGame() 
    {
        SceneManager.LoadScene(1);
        /*Modify it later to support save files -> 
        * start game opens saves menu with a "Start new game button"
        * clicking on a save file will well, do what it should. Load a save file.
        * new game will do this code
        */
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
