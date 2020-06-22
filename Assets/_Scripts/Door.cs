using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int costToOpen = 500;

    [SerializeField] private List<Transform> correspondingWindows;

    public void Unlock()
    {
        if (ScoreTextController.instance.gameScore >= costToOpen)
        {
            PlayerController.instance.ExitDoorEvent();

            ScoreTextController.instance.ScoreUp(-costToOpen);

            foreach (Transform spawnpoint in correspondingWindows)
            {
                bool isSpawnpointUsed = false;
                foreach (Transform listSpawnpoint in WaveManager.instance.defaultSpawnPoints)
                {
                    if (spawnpoint == listSpawnpoint)
                    {
                        isSpawnpointUsed = true;
                    }
                }

                if (!isSpawnpointUsed)
                {
                    WaveManager.instance.defaultSpawnPoints.Add(spawnpoint);
                }
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.InDoorEvent(costToOpen);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.ExitDoorEvent();
        }
    }
}
