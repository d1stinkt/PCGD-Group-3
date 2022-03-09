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

    public float fadeSpeed = 2f;

    [SerializeField] int s;

    [SerializeField] StartGame startGame;

    public static Menu instance;

    private void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        StartCoroutine(AudioManager.FadeIn("Menu", fadeSpeed));
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
