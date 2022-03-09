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
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void BackToMenu()
    {
        StartCoroutine(LoadMenu(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadLevel(int index)
    {
        animator.SetTrigger("StartLevel");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
        AudioManager.Play("Theme");
        animator.SetTrigger("Stop");
    }

    IEnumerator LoadMenu(int index)
    {
        animator.SetTrigger("StartLevel");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
        AudioManager.Play("Menu");
        animator.SetTrigger("Stop");
    }
}
