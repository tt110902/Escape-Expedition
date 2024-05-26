using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWin : MonoBehaviour
{
    public GameObject winPanel;
    public GameDataManager gameDataManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Open(winPanel);
            string currentSceneName = SceneManager.GetActiveScene().name;
            gameDataManager.CompleteLevel(currentSceneName, 0);
        }
    }


    public void Open(GameObject panel)
    {
        if (panel != null)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }
    }
}
