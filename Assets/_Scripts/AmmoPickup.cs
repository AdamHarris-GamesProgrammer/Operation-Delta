using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.instance.go.GetComponentInChildren<PlayerShoot>().AddAmmo(ammoAmount);

            Destroy(this.gameObject);
        }
    }
}
