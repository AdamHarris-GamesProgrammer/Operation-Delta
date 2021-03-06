﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [Header("Layer Settings")]
    [SerializeField] private LayerMask layer;

    private Animation anim;

    [Header("Attack Settings")]
    [SerializeField] private float meleeCooldown = 0.2f;
    [SerializeField] private float swordDamage = 12.0f;
    [SerializeField] private float attackRange = 3.5f;

    private float cooldownTimer = 0.0f;

    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    private Camera camera;

    bool canAttack = true;

    private void Awake()
    {
        camera = gameObject.GetComponentInParent<Camera>();
        meshRenderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isAlive)
        {
            if (!canAttack)
            {
                cooldownTimer += Time.deltaTime;
                if (cooldownTimer >= meleeCooldown)
                {
                    canAttack = true;
                }
            }

            if (anim.isPlaying)
            {
                meshRenderer.enabled = true;
            }
            else
            {
                meshRenderer.enabled = false;
            }

            Debug.DrawRay(camera.transform.position, camera.transform.forward * 2.5f, Color.green);

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (canAttack)
                {
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                    }
                    anim.Play("Sword");
                    Attack();
                    canAttack = false;
                }
            }
        }
       
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(camera.transform.position, attackRange, layer);
        
        for(int i = 0; i < hitColliders.Length; i++)
        {
            Enemy enemyComponent;
            hitColliders[i].gameObject.TryGetComponent<Enemy>(out enemyComponent);
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(swordDamage);
            }
        }
    }
}
