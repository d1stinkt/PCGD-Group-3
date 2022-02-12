using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    //public Text scoreText;

    public void ShowMenu()//int points
    {
        gameObject.SetActive(true);
        //scoreText.text = points.ToString() + " SCORE";
        //needs some more work to show points when dead since above doesn't work
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
