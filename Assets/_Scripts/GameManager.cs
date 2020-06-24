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

    [HideInInspector] public float levelTimer;

    enum GameMode { Surivial, RoomClear };
    [SerializeField] private GameMode gameMode;


    public GameObject ammoPrefab;
 
    public bool spawningEnabled = false;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverBgObject;
    [SerializeField] private Image gameOverBgPanelImage;

    [SerializeField] private float probabilityOfPickup = 0.05f;
    [SerializeField] private List<GameObject> pickups;

    [Header("Pickup Text")]
    [SerializeField] private Text pickupTrailText;

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
    }

    void Update()
    {
        levelTimer += Time.deltaTime;
    }

    public void EnemyKilled(Vector3 enemyPosition)
    {
        if (PlayerController.instance.killsSinceLastAmmoPickup >= 6)
        {
            //Instantiate a ammo pickup
            GameObject ammoPickup = Instantiate(GameManager.instance.ammoPrefab);
            ammoPickup.transform.position = new Vector3(enemyPosition.x, 0.1f, enemyPosition.z);

            PlayerController.instance.killsSinceLastAmmoPickup = 0;
        }

        float chance = UnityEngine.Random.Range(0.0f, 1.0f);

        if(chance <= probabilityOfPickup)
        {
            //Decide what pickup to spawn
            int index = UnityEngine.Random.Range(0, pickups.Count);

            GameObject pickup = Instantiate(pickups[index]);
            pickup.transform.position = new Vector3(enemyPosition.x, 0.1f, enemyPosition.z);
            Debug.Log("Spawning: " + pickup.name);

        }
    }

    public void OnDeath()
    {
        isGameOver = true;

        Cursor.lockState = CursorLockMode.None;

        StartCoroutine("FadeIn");
        StartCoroutine("FadeOut");
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

    public void PickupActive()
    {
        pickupTrailText.gameObject.SetActive(true);
    }

    public void PickupDisabled()
    {
        pickupTrailText.gameObject.SetActive(false);
    }
}
