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
    public bool armor = false;
    bool speedUp = false;
    [SerializeField]
    float speedMultiplier;

    [SerializeField]
    GameObject[] powerUps;
    [SerializeField]
    Vector3[] powerUpSpawns;
    int powerUpCount = 0;

    void Start()
    {
        Application.targetFrameRate = 60;
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        highScore = scoreSystem.LoadScore();
        highScoreTxt.text = "Highscore: " + highScore.ToString();
        InvokeRepeating("SpawnPowerUp", 1f, 10f);
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

    void SpawnPowerUp()
    {
        if (powerUpCount >= 5) { return; }
        powerUpCount++;
        Vector3 PowerUpSpawn = powerUpSpawns[Random.Range(0, powerUpSpawns.Length)];
        GameObject powerUp = powerUps[Random.Range(0, powerUps.Length)];
        Instantiate(powerUp, PowerUpSpawn, powerUp.transform.rotation);
    }

    //Power ups
    public IEnumerator PowerUp(int id)
    {
        Debug.Log("Test");
        powerUpCount--;
        switch (id)
        {
            case 0:
                if (rainbowBullet) { break; }
                rainbowBullet = true;
                yield return new WaitForSeconds(5);
                rainbowBullet = false;
                break;

            case 1:
                if (speedUp) { break; }
                speedUp = true;
                player.GetComponent<Player>().moveSpeed *= speedMultiplier;
                yield return new WaitForSeconds(5);
                player.GetComponent<Player>().moveSpeed /= speedMultiplier;
                speedUp = false;
                break;

            case 2:
                if (armor) { break; }
                armor = true;
                yield return new WaitForSeconds(5);
                armor = false;
                break;

            default:
                Debug.LogError("Power-up ID not recognized");
                break;
        }
    }
}

public class Global
{
    public static bool GamePaused;
}
