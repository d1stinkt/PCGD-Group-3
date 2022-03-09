using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScreen : MonoBehaviour
{
    public Text scorePauseText;
    private bool GameIsPaused;

    AudioManager AudioManager;
    [SerializeField] StartGame startGame;

    private void Start()
    {
        GameIsPaused = Global.GamePaused;
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
            gameObject.SetActive(false);
        }
    }

    public void ShowPauseMenu(int points)
    {
        gameObject.SetActive(true);
        scorePauseText.text = "WAVE: " + points.ToString();
    
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
        startGame.BackToMenu();
        AudioManager.EnableMenu();
        AudioManager.Pause("Theme");
    }
}
