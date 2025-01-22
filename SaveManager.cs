using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class SaveManager : MonoBehaviour
{
    [System.Serializable]
    public class GameData
    {
        public int bottlesHit;
        public Vector3 spawnPosition;
        public GameData()
        {
            bottlesHit = 0;
            spawnPosition = new Vector3(-0.109f, 1.5f, -8.575f);
        }
    }

    [SerializeField] private TMP_Text hitValue;
    public static SaveManager Instance { get; private set; } // Static object of the class.
    public GameData gameData;
    private string filePath;



    private void Awake() // Ensure there is only one instance of SoundManager.
    {
        if (Instance == null) // If the object/instance doesn't exist yet.
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // SoundManager will persist between scenes.
            Initialize();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Initialize the SoundManager. I just put this functionality here instead of in the static constructor.
    private void Initialize()
    {
        gameData = new GameData();
        LoadGameData();
    }

    private void LoadGameData()
    {
        // Define the file path for the JSON file. Path.Combine is preferred over concatenation (+).
        filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        Debug.Log(filePath);

        // Check if the file exists.
        if (File.Exists(filePath))
        {
            // Read the JSON data from the file.
            string jsonData = File.ReadAllText(filePath);

            // Deserialize the JSON data into our data class.
            gameData = JsonUtility.FromJson<GameData>(jsonData);

            // Update the highscore value right from the game data.
            hitValue.text = gameData.bottlesHit.ToString();
        }
        else
        {
            Debug.Log("No high score data to load.");
        }
    }

    private void SaveGameData()
    {
        // Serialize the data to JSON format.
        string jsonData = JsonUtility.ToJson(gameData);

        // Write the JSON data to the file.
        File.WriteAllText(filePath, jsonData);
    }

    public void UpdateScore()
    {
        gameData.bottlesHit++; ;
        hitValue.text = gameData.bottlesHit.ToString();
    }

    public void UpdateSpawnPoint(Vector3 newSpawn)
    {
        gameData.spawnPosition = newSpawn;
    }

    public void ClearGameData()
    {
        gameData.bottlesHit = 0;
        hitValue.text = gameData.bottlesHit.ToString();
        gameData.spawnPosition = new Vector3(-0.109f, 1.5f, -8.575f);
        if (File.Exists(filePath))
        {
            // Delete the JSON file.
            File.Delete(filePath);
            Debug.Log("Game data cleared successfully.");
        }
        else
        {
            Debug.Log("No game data to clear.");
        }
    }

    void OnApplicationQuit()
    {
        SaveGameData();
    }
}