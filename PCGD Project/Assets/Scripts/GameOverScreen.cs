using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;

    public void ShowMenu(int points)
    {
        gameObject.SetActive(true);
        scoreText.text = points.ToString() + " SCORE";
    }
}
