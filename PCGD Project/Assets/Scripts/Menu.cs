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

    ScoreSystem scoreSystem;

    [SerializeField]
    int s;

    StartGame startGame;

    private void Start()
    {
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        startGame = GameObject.Find("GameBegin").GetComponent<StartGame>();
        
    }

    public void Play()
    {
        startGame.LevelBegin();
        
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
