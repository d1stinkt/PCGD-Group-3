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
    public bool speedUp = false;
    [SerializeField]
    float speedMultiplier;

    public GameObject speedBar;
    public GameObject rainbowBar;
    public GameObject armorIcon;

    [SerializeField]
    GameObject[] powerUps;
    [SerializeField]
    Vector3[] powerUpSpawns;

    void Start()
    {
        Application.targetFrameRate = 60;
        scoreSystem = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>();
        highScore = scoreSystem.LoadScore();
        highScoreTxt.text = "HIGHSCORE: " + highScore.ToString();
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

        scoreTxt.text = "CURRENT WAVE: " + score.ToString();
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
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length >= 5) { return; }
        Vector3 PowerUpSpawn = powerUpSpawns[Random.Range(0, powerUpSpawns.Length)];
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            if (PowerUpSpawn == g.transform.position) { SpawnPowerUp(); return; }
        }

        if (armor)
        {
            GameObject powerUp = powerUps[Random.Range(0, powerUps.Length - 1)];
            Instantiate(powerUp, PowerUpSpawn, powerUp.transform.rotation);
        }
        else
        {
            GameObject powerUp = powerUps[Random.Range(0, powerUps.Length)];
            Instantiate(powerUp, PowerUpSpawn, powerUp.transform.rotation);
        }
    }

    //Power ups
    public IEnumerator PowerUp(int id)
    {
        switch (id)
        {
            case 0:
                if (rainbowBullet) { break; }
                rainbowBullet = true;
                rainbowBar.SetActive(true);
                yield return new WaitForSeconds(5);
                rainbowBar.SetActive(false);
                rainbowBullet = false;
                break;

            case 1:
                if (speedUp) { break; }
                speedUp = true;
                player.GetComponent<Player>().moveSpeed *= speedMultiplier;
                speedBar.SetActive(true);
                yield return new WaitForSeconds(5);
                speedBar.SetActive(false);
                player.GetComponent<Player>().moveSpeed /= speedMultiplier;
                speedUp = false;
                break;

            case 2:
                if (armor) { break; }
                armorIcon.SetActive(true);
                armor = true;
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
