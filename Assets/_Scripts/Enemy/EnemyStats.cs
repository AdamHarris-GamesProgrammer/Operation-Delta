using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy/Stats File")]
public class EnemyStats : ScriptableObject
{
    [Header("Health Settings")]
    public float health = 100.0f;

    [Header("Health Regeneration Settings")]
    public bool healthRegenEnabled = false;
    public float healthRegenRate = 2.5f;
    public float regenerationCooldown = 5.0f;

    [Header("Damage Settings")]
    public float damage = 12.0f;
    public float damageMultiplier = 1.0f;
    public float attackRate = 1.0f;
    public float attackRange = 1.5f;
    

    [Header("Movement Speed")]
    public float movementSpeed = 2.0f;
    public float sprintingSpeedFactor = 1.5f;
    public float crawlingSpeedFactor = 0.5f;
    public float damagedSpeedFactor = 0.5f;
    public bool canCrawl = false;
    public bool canSprint = false;

    [Header("Score Settings")]
    public int scoreValue = 100;

    [Header("Sound Settings")]
    public AudioClip spawnSound;
    public AudioClip damagedSound;
    public AudioClip attackSound;
    public AudioClip deathSound;

    [Header("Knockback Settings")]
    public float knockbackTime = 0.2f;

}
