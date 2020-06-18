using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullHealthPickup : Pickup
{
    protected override void PickupAction()
    {
        PlayerController.instance.RefillHealth();
    }
}
