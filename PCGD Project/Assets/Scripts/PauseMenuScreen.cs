using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScreen : MonoBehaviour
{
    public Text scorePauseText;
    private bool GameIsPaused;
    public float fadeSpeed = 1.5f;

    AudioManager AudioManager;

    [SerializeField] StartGame startGame;

    private void Start()
    {
        GameIsPaused = Global.GamePaused;
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        startGame = GameObject.Find("GameBegin").GetComponent<StartGame>();
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
        GameIsPaused = true;
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
        
        GameIsPaused = true;
        StartCoroutine(AudioManager.FadeIn("Menu", fadeSpeed));
        startGame.BackToMenu();
        AudioManager.EnableMenu();
        
    }
}
