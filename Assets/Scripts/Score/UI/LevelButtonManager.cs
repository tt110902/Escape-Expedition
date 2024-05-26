using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    public GameObject buttonPrefab; // Prefab của nút
    public Transform buttonContainer; // Panel chứa các nút
    public GameDataManager gameDataManager; // Tham chiếu đến GameDataManager để lấy dữ liệu

    void Start()
    {
        // Load dữ liệu từ GameDataManager
        gameDataManager.LoadGameData();

        // Tạo các nút dựa trên dữ liệu
        CreateLevelButtons();
    }

    private void OnEnable()
    {
        CreateLevelButtons();
    }

    public void CreateLevelButtons()
    {
        foreach (Transform child in transform) GameObject.Destroy(child.gameObject);

        bool anyLevelCompleted = false;
        bool previousLevelCompleted = false;
        for (int i = 0; i < gameDataManager.gameManager.Levels.Count; i++)
        {
            var level = gameDataManager.gameManager.Levels[i];
            GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = level.Name;

            // Kiểm tra xem màn trước đã hoàn thành chưa
            if (previousLevelCompleted || i == 0)
            {
                button.interactable = true;
                previousLevelCompleted = level.IsCompleted;
            }
            else
            {
                button.interactable = false;
            }

            // Thêm sự kiện để chuyển đến scene tương ứng khi nút được nhấn
            string sceneName = level.Name; // Giả sử tên màn chơi cũng là tên của scene
            button.onClick.AddListener(() => LoadLevel(sceneName));

            // Đánh dấu rằng có một màn đã được hoàn thành
            if (level.IsCompleted)
            {
                anyLevelCompleted = true;
            }
        }

        // Nếu chưa có màn nào được hoàn thành, đảm bảo nút đầu tiên luôn được bật (enable)
        if (!anyLevelCompleted && gameDataManager.gameManager.Levels.Count > 0)
        {
            buttonContainer.GetChild(0).GetComponent<Button>().interactable = true;
        }
    }

    void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
