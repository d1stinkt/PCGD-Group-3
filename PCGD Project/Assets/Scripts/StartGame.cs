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

    public void Start()
    {
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
        StartCoroutine(AudioManager.FadeOut("Menu", fadeSpeed));
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    public void BackToMenu()
    {
        StartCoroutine(AudioManager.FadeOut("Theme", fadeSpeed));
        StartCoroutine(LoadMenu(SceneManager.GetActiveScene().buildIndex - 1));
        

    }

    IEnumerator LoadLevel(int index)
    {
        StartCoroutine(AudioManager.FadeIn("Theme", fadeSpeed));
        animator.SetTrigger("StartLevel");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
        animator.SetTrigger("Stop");
    }

    IEnumerator LoadMenu(int index)
    {
        
        animator.SetTrigger("StartLevel");
        yield return new WaitForSeconds(1.5f);      
        SceneManager.LoadScene(index);
        StartCoroutine(AudioManager.FadeIn("Menu", fadeSpeed));
        animator.SetTrigger("Stop");
    }
}
