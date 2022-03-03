using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Animator animator;
    float transitionTime = 1.5f;

    public void LevelBegin()
    {
        animator.SetTrigger("StartLevel");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int index)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
}