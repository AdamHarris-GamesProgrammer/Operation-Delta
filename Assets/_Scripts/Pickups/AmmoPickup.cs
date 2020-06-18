using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] private int ammoAmount = 30;

    protected override void PickupAction() 
    {
        PlayerController.instance.go.GetComponentInChildren<PlayerShoot>().AddAmmo(ammoAmount);
    }
}
