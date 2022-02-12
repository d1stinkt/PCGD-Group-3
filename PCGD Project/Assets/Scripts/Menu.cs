using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Text score;

    [SerializeField]
    ScoreSystem scoreSystem;

    [SerializeField]
    int s;

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Debug.Log("Quit");

        Application.Quit();
    }


    public void GetScore()
    {
        score.text = Convert.ToString(scoreSystem.LoadScore());
    }

    public void SaveScore()
    {
        scoreSystem.SaveScore(s);
    }
}
