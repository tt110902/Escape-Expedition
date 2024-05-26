using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public GameManager gameManager;
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "gameData.xml");

        // Kiểm tra xem file đã tồn tại chưa, nếu chưa thì tạo dữ liệu mặc định
        if (File.Exists(filePath))
        {
            LoadGameData();
        }
        else
        {
            CreateDefaultGameData();
            SaveGameData();
        }

        // Hiển thị dữ liệu đã tải
        foreach (var level in gameManager.Levels)
        {
            Debug.Log($"Name: {level.Name}, IsCompleted: {level.IsCompleted}, Score: {level.Score}");
        }
    }

    void CreateDefaultGameData()
    {
        gameManager = new GameManager();
        gameManager.Levels.Add(new Level { Name = "Scene1", IsCompleted = false, Score = 0 });
        gameManager.Levels.Add(new Level { Name = "Scene2", IsCompleted = false, Score = 0 });
        gameManager.Levels.Add(new Level { Name = "Scene3", IsCompleted = false, Score = 0 });
        gameManager.Levels.Add(new Level { Name = "Scene4", IsCompleted = false, Score = 0 });
        gameManager.Levels.Add(new Level { Name = "Scene5", IsCompleted = false, Score = 0 });
        gameManager.Levels.Add(new Level { Name = "Scene6", IsCompleted = false, Score = 0 });
    }

    public void SaveGameData()
    {
        gameManager.SaveToFile(filePath);
        Debug.Log($"Data saved to {filePath}");
    }

    public void LoadGameData()
    {
        gameManager = GameManager.LoadFromFile(filePath);
        Debug.Log("Data loaded from file");
    }

    public void CompleteLevel(string levelName, int score)
    {
        Level level = gameManager.Levels.Find(l => l.Name == levelName);
        if (level != null)
        {
            level.IsCompleted = true;
            level.Score = score;
            SaveGameData();
            Debug.Log($"Level {levelName} marked as completed.");
        }
        else
        {
            Debug.LogWarning($"Level {levelName} not found.");
        }
    }

    public void ClearGameData()
    {
        CreateDefaultGameData();
        SaveGameData();
        Debug.Log("All game data cleared and reset to default.");
    }
}
