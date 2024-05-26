using UnityEngine;
using UnityEngine.UI;

public class ClosePanel : MonoBehaviour
{
    public Button button;
    public GameObject panel;

    void Start()
    {
        if (button != null) button.onClick.AddListener(() => Close(panel));
    }

    public void Close(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
