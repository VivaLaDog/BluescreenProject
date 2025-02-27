using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            Destroy(Instance);
        }
        Instance = this;
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistentObjects();
        LoadGame();
    }


    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //loads data from a serialized json file
        this.gameData = dataHandler.Load();


        if(this.gameData == null)
        {
            Debug.Log("No data found, initializing to default.");
            NewGame();
        }

        foreach (IDataPersistence data in dataPersistenceObjects)
        {
            data.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //saves data to a serialized json file
        foreach (IDataPersistence data in dataPersistenceObjects)
        {
            data.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistentObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = 
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
