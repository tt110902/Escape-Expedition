using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public Button button;

    void Start()
    {
        if (button != null) button.onClick.AddListener(() => Quit());
    }


    public void Quit()
    {
        Debug.Log("Quit Game");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
