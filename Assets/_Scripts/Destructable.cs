using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;

    public void TakeDamage(float damageIn)
    {
        health -= damageIn;
        if(health <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
