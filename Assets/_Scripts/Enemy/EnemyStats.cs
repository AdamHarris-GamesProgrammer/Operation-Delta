using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("Movement Settings")]
    public float moveSpeed = 1.0f;

    [Header("Look Settings")]
    public float lookRange = 40.0f;
    public float lookSphereCastRadius = 1.0f;

    [Header("Attack Settings")]
    public float attackRange = 1.0f;
    public float attackRate = 1.0f;
    public float attackForce = 15.0f;
    public int attackDamage = 50;

    [Header("Search Settings")]
    public float searchDuration = 4.0f;
    public float searchingTurnSpeed = 120.0f;
}
