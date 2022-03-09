using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    AudioManager AudioManager;
    [SerializeField] Animator animator;
    float transitionTime = 1.5f;

    public static StartGame startGame;

    public float fadeSpeed = 1.5f;
    private bool GameIsPaused;

    public void Start()
    {
        GameIsPaused = Global.GamePaused;
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    void Awake()
    {
        if (startGame == null)
            startGame = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LevelBegin()
    {
        GameIsPaused = false;
        StartCoroutine(AudioManager.FadeOut("Menu", fadeSpeed));
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    public void BackToMenu()
    {
        GameIsPaused = true;
        Time.timeScale = 1f;
        StartCoroutine(LoadMenu(SceneManager.GetActiveScene().buildIndex - 1));
        AudioManager.Pause("Theme");
        StartCoroutine(AudioManager.FadeIn("Menu", fadeSpeed));

    }

    IEnumerator LoadLevel(int index)
    {
        GameIsPaused = false;
        StartCoroutine(AudioManager.FadeIn("Theme", fadeSpeed));
        animator.SetTrigger("StartLevel");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
        animator.SetTrigger("Stop");
    }

    IEnumerator LoadMenu(int index)
    {
        GameIsPaused = true;
        animator.SetTrigger("StartLevel");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
        animator.SetTrigger("Stop");
    }
}
