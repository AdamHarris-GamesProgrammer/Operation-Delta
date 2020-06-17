using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int costToOpen = 500;

    [SerializeField] private List<Transform> correspondingWindows;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock()
    {
        if (ScoreTextController.instance.gameScore >= costToOpen)
        {
            Debug.Log("Unlock Called");

            ScoreTextController.instance.ScoreUp(-costToOpen);

            foreach (Transform spawnpoint in correspondingWindows)
            {
                bool isSpawnpointUsed = false;
                foreach (Transform listSpawnpoint in GameManager.instance.defaultSpawnPoints)
                {
                    if (spawnpoint == listSpawnpoint)
                    {
                        isSpawnpointUsed = true;
                    }
                }

                if (!isSpawnpointUsed)
                {
                    GameManager.instance.defaultSpawnPoints.Add(spawnpoint);
                }
            }

            Destroy(this.gameObject);
        }
    }
}
