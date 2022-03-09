using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    AudioManager AudioManager;
    [SerializeField] Animator animator;
    float transitionTime = 1.5f;

    public void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void LevelBegin()
    {
        animator.SetTrigger("StartLevel");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void BackToMenu()
    {
        animator.SetTrigger("StartLevel");
        StartCoroutine(LoadMenu(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadLevel(int index)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
        AudioManager.Play("Theme");
    }

    IEnumerator LoadMenu(int index)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(index);
        AudioManager.Play("Menu");
    }
}
