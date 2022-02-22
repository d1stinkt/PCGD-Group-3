using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScreen : MonoBehaviour
{
    public Text scorePauseText;
    private bool GameIsPaused;
    


    private void Start()
    {
        GameIsPaused = Global.GamePaused;
        
    }

    public void ShowPauseMenu(int points)
    {
        gameObject.SetActive(true);
        scorePauseText.text = "SCORE: " + points.ToString();
    
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        gameObject.SetActive(false);
        
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
