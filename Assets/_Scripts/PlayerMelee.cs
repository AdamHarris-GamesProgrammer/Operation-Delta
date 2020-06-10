using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [Header("Layer Settings")]
    [SerializeField] private LayerMask layer;

    public Animation sword;


    [SerializeField] private float meleeCooldown = 0.2f;
    private float cooldownTimer = 0.0f;

    private MeshRenderer meshRenderer;

    private Camera camera;

    bool canAttack = true;

    private void Awake()
    {
        camera = gameObject.GetComponentInParent<Camera>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack)
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer >= meleeCooldown)
            {
                canAttack = true;
            }
        }

        if (sword.isPlaying)
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
                //meshRenderer.enabled = true;
                sword.Play("Sword");
                Attack();
                canAttack = false;
            }
        }
    }

    void Attack()
    {
        //TODO: Play attack sound
        //TODO: Add in slash animation

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 3.5f, layer))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(2.0f);
            }
        }
    }
}
