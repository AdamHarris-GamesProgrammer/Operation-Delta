using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [HideInInspector] public int waveNumber = 1;
    [SerializeField] private int baseAmountOfEnemies = 10;
    [SerializeField] private float timeBetweenEachSpawnMin = 3.5f;
    [SerializeField] private float timeBetweenEachSpawnMax = 12.0f;
    [SerializeField] private float enemyIncreasePerWave = 1.2f;
    [SerializeField] private float timeBetweenWaves = 5.0f;

    public List<GameObject> enemyPrefabs;
    public List<Transform> defaultSpawnPoints;

    [Header("UI Settings")]
    [SerializeField] private Text waveText;
    [SerializeField] private Text waveTimerText;
    [SerializeField] private GameObject waveCompleteGO;

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

        waveText.text = "Wave: 1";
        waveNumber = 1;
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

            waveTimer -= Time.deltaTime;
            waveTimerText.text = waveTimer.ToString("F2") + "s";

            if(waveTimer <= 0.0f)
            {
                waveOver = false;
                bCalled = false;
                waveTimer = 0;

                waveTimerText.gameObject.SetActive(false);
                waveCompleteGO.SetActive(false);
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

        waveText.text = "Wave: " + waveNumber;

        waveTimer = timeBetweenWaves;
        waveCompleteGO.SetActive(true);
        waveTimerText.text = "5.00s";
        waveTimerText.gameObject.SetActive(true);
    }
}
