using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenPickup : Pickup
{
    [SerializeField] private float effectDuration = 15.0f;

    protected override void PickupAction()
    {
        PlayerController.instance.SetEffectDuration(effectDuration);
    }
}
