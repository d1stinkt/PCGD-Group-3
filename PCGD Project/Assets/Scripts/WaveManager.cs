using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public SpawnState state = SpawnState.COUNTING;

    public Wave[] waves;
    private int nextWave = 0;
    public Transform[] spawnPoints;

    private float searchCD = 1f;    //timer for checking if enemies are alive
    float waveCD;
    public float timeBetweenWaves = 1f;

    public int waveEnemies;
    public GameObject[] typeOfEnemies;

    GameManager gm;

    [System.Serializable]
    public class Wave
    {
        public string waveName;     //for future name announcement
        public int numberOfEnemies;
        public float spawnInterval;
    }

    void Start()
    {
        waveCD = timeBetweenWaves;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemiesLeft())
            {
                SpawnNextWave();
            }
            else
            {
                return;
            }
        }
        if (waveCD <= 0)
        {
            //Debug.Log("Wavecounter down");
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCD -= Time.deltaTime;
        }
    }





    void SpawnNextWave()
    {
        state = SpawnState.COUNTING;
        waveCD = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave--;
            Debug.Log("Waves done. Looping...");    //gamestate complete! 
        }

        nextWave++;
        currentWave++;
        gm.score++;
    }

    bool EnemiesLeft()
    {
        searchCD -= Time.deltaTime;
        if (searchCD <= 0f)
        {
            searchCD = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }

        return true;


    }

    IEnumerator SpawnWave(Wave _wave)
    {
        waveEnemies = _wave.numberOfEnemies; // Grabbing numberOfEnemies to a wave specific variable
        state = SpawnState.SPAWNING;

        for (int i = 0; i < waveEnemies; i++)
        {
            SpawnEnemy(_wave);

            yield return new WaitForSeconds(_wave.spawnInterval);
        }


        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Wave _wave)
    {
        Transform blueSpawnPoint = spawnPoints[0];
        Transform yellowSpawnPoint = spawnPoints[1];
        Transform greenSpawnPoint = spawnPoints[2];
        Transform redSpawnPoint = spawnPoints[3];

        // Spawning random enemies to all of the spawn points
        Instantiate(typeOfEnemies[Random.Range(0,4)], blueSpawnPoint.position, blueSpawnPoint.rotation);
        Instantiate(typeOfEnemies[Random.Range(0,4)], redSpawnPoint.position, redSpawnPoint.rotation);
        Instantiate(typeOfEnemies[Random.Range(0,4)], greenSpawnPoint.position, greenSpawnPoint.rotation);
        Instantiate(typeOfEnemies[Random.Range(0,4)], yellowSpawnPoint.position, yellowSpawnPoint.rotation);

        waveEnemies--;
    }

}