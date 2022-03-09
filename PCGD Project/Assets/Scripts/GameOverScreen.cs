using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    AudioManager AudioManager;
    [SerializeField] StartGame startGame;
    
    public Text scoreText;

    public void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        startGame = GameObject.Find("GameBegin").GetComponent<StartGame>();
    }

    public void ShowMenu(int points)
    {
        gameObject.SetActive(true);
        scoreText.text = "SCORE: " + points.ToString();   
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenuButton()
    {
        startGame.BackToMenu();
        AudioManager.EnableMenu();
        AudioManager.Pause("Theme");
    }

}
