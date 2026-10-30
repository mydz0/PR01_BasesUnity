using TMPro;
using UnityEngine;                                     
using UnityEngine.SceneManagement;                     

public class MainMenuManager : MonoBehaviour        
{
    [SerializeField] private TextMeshProUGUI textTotalPoints;
    public void NewGame()                              
    {
        SceneManager.LoadScene(1);         // Carga la escena del juego
        PlayerStats.Instance.ResetPoints();
    }

    public void Continue()                             
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        int points = PlayerStats.Instance.GetTotalPoints();
        textTotalPoints.text = points+"";
    }

}
