using System;
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

    public List<GameObject> enemyPrefabs;

    enum GameMode { Surivial, RoomClear };
    [SerializeField] private GameMode gameMode;


    public GameObject spawnpointsParent;
    [HideInInspector] public List<Transform> spawnPoints;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverBgObject;


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
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform go in spawnpointsParent.transform)
        {
            spawnPoints.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= enemySpawnCooldown)
        {
            int spawnPoint = UnityEngine.Random.Range(0, spawnPoints.Count);
            int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);


            GameObject enemyInstance = Instantiate(enemyPrefabs[enemyIndex]);
            enemyInstance.transform.position = spawnPoints[spawnPoint].position;

            spawnTimer = 0.0f;

            Debug.Log("Enemy Spawned At: " + spawnPoints[spawnPoint].transform.name);
        }
    }

    void RoomClearUpdate()
    {

    }

    public void OnDeath()
    {
        isGameOver = true;

        gameOverBgObject.SetActive(true);
    }
}
