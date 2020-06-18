using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDamagePickup : Pickup
{
    [SerializeField] private float effectDuration = 15.0f;

    protected override void PickupAction()
    {
        PlayerController.instance.go.GetComponentInChildren<PlayerShoot>().damageFactor = 2.0f;
        PlayerController.instance.go.GetComponentInChildren<PlayerShoot>().SetEffectDuration(effectDuration);

    }
}
