using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform player, centerPoint;

    public int ColorID = 0;
    float[] location = { 0, 0 };

    public float bulletForce;

    ScoreSystem scoreSystem;
    public int score = 1;
    int highScore;

    [SerializeField]
    Text scoreTxt, highScoreTxt;

    public GameOverScreen GameOverScreen;
    public PauseMenuScreen PauseMenuScreen;

    public bool rainbowBullet = false;

    void Start()
    {
        Application.targetFrameRate = 60;
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        highScore = scoreSystem.LoadScore();
        highScoreTxt.text = "Highscore: " + highScore.ToString();
        
    }



    void Update()
    {
        centerPoint.LookAt(player);
        location[0] = centerPoint.localRotation.y / Mathf.Abs(centerPoint.localRotation.y);
        location[1] = -(centerPoint.localRotation.x / Mathf.Abs(centerPoint.localRotation.x));

        switch (location)
        {
            case var l when l[0] == 1 && l[1] == 1:         //Top-Right corner
                ColorID = 0;
                break;

            case var l when l[0] == -1 && l[1] == 1:        //Top-Left corner
                ColorID = 1;
                break;

            case var l when l[0] == 1 && l[1] == -1:        //Bottom-Right corner
                ColorID = 2;
                break;

            case var l when l[0] == -1 && l[1] == -1:       //Bottom-Left corner
                ColorID = 3;
                break;

            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Time.timeScale = 0f;
            PauseMenuScreen.ShowPauseMenu(score);

        }

        scoreTxt.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        GameOverScreen.ShowMenu(score);

        if (score > highScore)
        {
            scoreSystem.SaveScore(score);
        }

    }

    //Power ups
    public IEnumerator PowerUp(int id)
    {
        Debug.Log("Test1");
        switch (id)
        {
            case 0:
                Debug.Log("Test2");
                rainbowBullet = true;
                yield return new WaitForSeconds(5);
                rainbowBullet = false;
                Debug.Log("Test3");
                break;
        }
    }
}

public class Global
{
    public static bool GamePaused;
}
