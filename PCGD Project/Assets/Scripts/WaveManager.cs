using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int currentWave;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public SpawnState state = SpawnState.COUNTING;

    public Wave[] waves;
    private int nextWave = 0;
    public Transform[] spawnPoints;

    private float searchCD = 1f;    //timer for checking if enemies are alive
    public float waveCD;
    public float timeBetweenWaves = 1f;

    [System.Serializable]
    public class Wave
    {
        public string waveName;     //for future name announcement
        public int numberOfEnemies;
        public float spawnInterval;
        public GameObject[] typeOfEnemies;

    }

    void Start()
    {
        waveCD = timeBetweenWaves;
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
            nextWave = 0;
            Debug.Log("Waves done. Looping...");    //gamestate complete! 
        }

        nextWave++;
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
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.numberOfEnemies; i++)
        {
            SpawnEnemy(_wave.numberOfEnemies);      //why not works???

            yield return new WaitForSeconds(_wave.spawnInterval);
        }


        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Wave typeOfEnemies)     //this is just my idea of getting the enemies to spawn in right places... kinda complicated
    {
        GameObject blueEnemy = currentWave.typeOfEnemies[0];
        GameObject yellowEnemy = currentWave.typeOfEnemies[1];
        GameObject greenEnemy = currentWave.typeOfEnemies[2];
        GameObject redEnemy = currentWave.typeOfEnemies[3];

        Transform blueSpawnPoint = spawnPoints[0];
        Transform yellowSpawnPoint = spawnPoints[1];
        Transform greenSpawnPoint = spawnPoints[2];
        Transform redSpawnPoint = spawnPoints[3];

        Instantiate(blueEnemy, blueSpawnPoint.position);
        Instantiate(yellowEnemy, yellowSpawnPoint.position);
        Instantiate(greenEnemy, greenSpawnPoint.position);
        Instantiate(redEnemy, redSpawnPoint.position);

        currentWave.numberOfEnemies--;
    }

}