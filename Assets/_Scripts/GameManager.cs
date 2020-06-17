﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isGameOver { get; set; }

    public static GameManager instance;

    public float enemySpawnCooldown = 5.0f;
    private float spawnTimer = 0.0f;

    [HideInInspector] public float levelTimer;

    public List<GameObject> enemyPrefabs;

    enum GameMode { Surivial, RoomClear };
    [SerializeField] private GameMode gameMode;


    public List<Transform> defaultSpawnPoints;
    public bool spawningEnabled = false;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverBgObject;
    [SerializeField] private Image gameOverBgPanelImage;

    private NavMeshBaker baker;


    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        baker = GetComponent<NavMeshBaker>();
        baker.BakeMeshes();
    }

    public void DoorUnlocked()
    {
        baker.BakeMeshes();
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;

        if (!isGameOver)
        {
            if (gameMode == GameMode.Surivial)
            {
                SurvivalUpdate();
            }
            else if (gameMode == GameMode.RoomClear)
            {
                RoomClearUpdate();
            }
        }
    }

    void SurvivalUpdate()
    {
        if (spawningEnabled)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= enemySpawnCooldown)
            {
                int spawnPoint = UnityEngine.Random.Range(0, defaultSpawnPoints.Count);

                Debug.Log("Spawn point index: " + spawnPoint);
                int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);


                GameObject enemyInstance = Instantiate(enemyPrefabs[enemyIndex]);
                enemyInstance.transform.position = defaultSpawnPoints[spawnPoint].position;

                spawnTimer = 0.0f;

                Debug.Log("Enemy Spawned At: " + defaultSpawnPoints[spawnPoint].transform.name);
            }
        }

    }

    void RoomClearUpdate()
    {

    }

    public void OnDeath()
    {
        isGameOver = true;

        StartCoroutine("FadeIn");
        StartCoroutine("FadeOut");

        //gameOverBgObject.SetActive(true);
    }

    IEnumerator FadeIn()
    {
        for (float ft = 0f; ft < 0.8; ft += 0.1f)
        {
            Color c = gameOverBgPanelImage.color;
            c.a = ft;
            gameOverBgPanelImage.color = c;
            yield return new WaitForSeconds(.1f);
        }

        if(gameOverBgPanelImage.color.a >= 0.7f)
        {
            gameOverBgObject.SetActive(true);
        }
    }

    IEnumerator FadeOut()
    {
        for (float ft = 1f; ft >= -0.15; ft -= 0.15f)
        {
            Color c = ScoreTextController.instance.scoreText.color;
            c.a = ft;
            ScoreTextController.instance.scoreText.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}
