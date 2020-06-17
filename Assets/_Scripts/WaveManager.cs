using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [HideInInspector] public int waveNumber = 0;
    [SerializeField] private int baseAmountOfEnemies = 10;
    [SerializeField] private float timeBetweenEachSpawnMin = 1.2f;
    [SerializeField] private float timeBetweenEachSpawnMax = 7.0f;
    [SerializeField] private float enemyIncreasePerWave = 1.2f;
    [SerializeField] private float timeBetweenWaves = 5.0f;

    public List<GameObject> enemyPrefabs;
    public List<Transform> defaultSpawnPoints;

    private int enemiesLeft;
    bool waveOver = false;

    private float spawnTimer;

    public static WaveManager instance;

    float timeBetweenEnemySpawns;

    int totalAmountOfEnemies;
    int enemiesSpawned = 0;

    float waveTimer = 0.0f;
    bool bCalled = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        totalAmountOfEnemies = baseAmountOfEnemies;
        timeBetweenEnemySpawns = UnityEngine.Random.Range(timeBetweenEachSpawnMin, timeBetweenEachSpawnMax);
        enemiesLeft = totalAmountOfEnemies;
    }


    private void Update()
    {

        if (!waveOver)
        {
            if (enemiesSpawned <= totalAmountOfEnemies)
            {
                spawnTimer += Time.deltaTime;

                if (spawnTimer >= timeBetweenEnemySpawns)
                {
                    int spawnPoint = UnityEngine.Random.Range(0, defaultSpawnPoints.Count);

                    Debug.Log("Spawn point index: " + spawnPoint);
                    int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);


                    GameObject enemyInstance = Instantiate(enemyPrefabs[enemyIndex]);
                    enemyInstance.transform.position = defaultSpawnPoints[spawnPoint].position;

                    spawnTimer = 0.0f;

                    enemiesSpawned++;
                }
            }

            if (enemiesLeft == 0 && !waveOver)
            {
                waveOver = true;
            }
        }
        else
        {
            if (!bCalled)
            {
                NextWave();
            }

            waveTimer += Time.deltaTime;

            if(waveTimer >= timeBetweenWaves)
            {
                waveOver = false;
                bCalled = false;
                waveTimer = 0;
            }

        }

    }

    public void EnemyKilled()
    {
        enemiesLeft--;
    }

    void NextWave()
    {
        bCalled = true;
        waveNumber++;

        if(waveNumber > 5)
        {
            enemyIncreasePerWave = 1.2f;
        }
        else if(waveNumber > 10)
        {
            enemyIncreasePerWave = 1.1f;
        }
        else if(waveNumber > 25)
        {
            enemyIncreasePerWave = 1.05f;
        }

        baseAmountOfEnemies = Mathf.RoundToInt(baseAmountOfEnemies * enemyIncreasePerWave);
        totalAmountOfEnemies = baseAmountOfEnemies;
        enemiesSpawned = 0;
        enemiesLeft = baseAmountOfEnemies;

        Debug.Log("Wave Number: " + waveNumber + "\tAmount of Enemies: " + totalAmountOfEnemies);
    }
}
