using UnityEngine;
using UnityEngine.UI;

public class OpenPanel : MonoBehaviour
{
    public Button button;
    public GameObject panel;

    void Start()
    {
        if (button != null) button.onClick.AddListener(() => Open(panel));
    }
    public void Open(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
