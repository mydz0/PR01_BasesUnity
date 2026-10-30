using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] TMP_Text TextCoinUI;

    void Start()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        if (PlayerStats.Instance != null)
        {
            UpdateCoinPoints(PlayerStats.Instance.GetTotalPoints());
        }

    }

    public void ClickPause()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ClickResume()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ClickRestart()
    {
        Time.timeScale = 1f;
        if (PlayerStats.Instance != null) PlayerStats.Instance.ResetPoints();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClickQuit()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateCoinPoints(int points)
    {
        if (TextCoinUI != null) TextCoinUI.text = points.ToString();
    }
}
