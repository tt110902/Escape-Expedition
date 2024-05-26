using UnityEngine;

public class CheckLoss : MonoBehaviour
{

    public GameObject lossPanel;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Open(lossPanel);
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
