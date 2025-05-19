using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage")]
    [SerializeField] private string fileName;


    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager Instance {  get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log($"There are too many instances of data persistence manager! Deleting newest one...");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistentObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        dataHandler.Save(gameData);
    }

    public void ContinueGame()
    {
        LoadGame();

        SceneManager.LoadSceneAsync(gameData.sceneIndex);
    }
    public void LoadGame()
    {
        //loads data from a serialized json file
        this.gameData = dataHandler.Load();


        if(this.gameData == null)
        {
            Debug.Log("No data found, please start a new game first.");
            return;
        }

        foreach (IDataPersistence data in dataPersistenceObjects)
        {
            data.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.Log("No data found, please start a new game first.");
            return;
        }

        //saves data to a serialized json file
        foreach (IDataPersistence data in dataPersistenceObjects)
        {
            data.SaveData(ref gameData);
        }
        if(gameData != null)
        dataHandler.Save(gameData);
    }

    /*private void OnApplicationQuit() //KEEP FOR DEBUGGING, DELETE FOR BUILD
    {
        SaveGame();
    }*/
    private List<IDataPersistence> FindAllDataPersistentObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = 
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null && dataHandler.Load().interactedWith.Count > 0;
    }

    internal void ClearGameData()
    {
        this.gameData = new GameData();
    }
}
