using UnityEngine;
using System.IO;
using TMPro;

[System.Serializable] // For JSON.
public class GameData
{
    public int BS; // Bottle score
    public Vector3 spawnPosition;
    public Vector3 Startspawn { get; private set; } // Now it's a property!

    public GameData()
    {
        BS = 0;
        Startspawn = new Vector3(-0.109f, 1.5f, -8.575f); // Starting position of controller object 
        spawnPosition = Startspawn; 
    }
}
public class SaveManager : MonoBehaviour
{
    [SerializeField] private TMP_Text hitValue;
    public static SaveManager instance { get; private set; }
    // Space for instance
    public GameData gameData;
    private string filePath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional becaus eonly one scene.
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Initialize()
    {
        gameData = new GameData();// Create the object of GameData
        LoadGameData();
    }

    private void LoadGameData()
    {
        filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(jsonData);
            hitValue.text = gameData.BS.ToString();
        }
        else
            Debug.Log("No high score data found");
    }
    private void SaveGameData()
    {
        string jsonData = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, jsonData);
    }
    public void ClearGameData() // Public cuz its a button function
    {
        gameData.BS = 0;
        hitValue.text = "0";
        gameData.spawnPosition = gameData.Startspawn;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Game data succesfully cleared");
        }
        else
            Debug.Log("No game data to clear!");
    }
    public void UpdateScore()
    {
        gameData.BS++; // Or you can give as many points as you like
        hitValue.text = gameData.BS.ToString();
    }
    public void UpdateSpawnPoint(Vector3 newSpawn)
    {
        gameData.spawnPosition = newSpawn; // Set spawn to a new checkpoint
    }
    public void OnApplicationQuit() // Automatically run when the game quits
    {
        SaveGameData();
    }
}
