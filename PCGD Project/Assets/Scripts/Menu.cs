using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    AudioManager AudioManager;

    [SerializeField] Text score;

    ScoreSystem scoreSystem;

    [SerializeField] int s;

    StartGame startGame;

    public static Menu instance;

    private void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        startGame = GameObject.Find("GameBegin").GetComponent<StartGame>();
        AudioManager.Play("Menu");   
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        AudioManager.Pause("Menu");
        startGame.LevelBegin();
        Disable();
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

    public void Disable()
    {
        StartCoroutine(DisableMenu());
    }

    IEnumerator DisableMenu()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
