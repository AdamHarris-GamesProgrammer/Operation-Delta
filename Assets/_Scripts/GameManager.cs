using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isGameOver { get; set; }

    public static GameManager instance;

    public float enemySpawnCooldown = 5.0f;
    private float spawnTimer = 0.0f;

    public List<GameObject> enemyPrefabs;

    //TODO: Detect spawn points on Start function
    public List<Transform> spawnPoints;

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

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= enemySpawnCooldown)
        {
            int spawnPoint = UnityEngine.Random.Range(0, spawnPoints.Count);
            int enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);


            GameObject enemyInstance = Instantiate(enemyPrefabs[enemyIndex]);
            enemyInstance.transform.position = spawnPoints[spawnPoint].position;

            spawnTimer = 0.0f;

            Debug.Log("Enemy Spawned At: " + spawnPoints[spawnPoint].transform.name);
        }
    }
}
