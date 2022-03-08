using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    AudioManager AudioManager;
    
    public Text scoreText;

    public void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        SceneManager.LoadScene("Menu");
        AudioManager.Pause("Theme");
    }

}
