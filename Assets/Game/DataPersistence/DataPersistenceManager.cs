using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    public List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void Start()
    {
        
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void NewGame2()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No data was found!");
            //NewGame();
            NewGame2();
        }

        //foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        //{
        //    dataPersistenceObj.LoadData(gameData);
        //}
        //Debug.Log("Loaded Game " + this.gameData.ListInventory.Count);
        //Debug.Log("Loaded Game " + this.gameData.Gold);
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        Debug.Log("SAVED GAME");

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public List<string> GetListInventoryName()
    {
        return this.gameData.ListInventory;
    }

    public List<CharacterData> GetListCharacterData()
    {
        return this.gameData.ListCharacter;
    }

    public int GetGold()
    {
        return this.gameData.Gold;
    }

    public void AddInventory(string nameInventory)
    {
        this.gameData.ListInventory.Add(nameInventory);
    }
}
