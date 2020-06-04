using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerShoot : MonoBehaviour
{
    [Header("Layer Settings")]
    [SerializeField] private LayerMask layer;

    [Header("Gun Settings")]
    [SerializeField] private float range = 100.0f;
    [Tooltip("Amount of bullets fired per second")]
    [SerializeField] private float fireRate = 5.0f;

    float timeSinceLastShot;
    float timeNeededBetweenShots;

    //Magazine size
    //Reload
    //Recoil
    //Damage
    //Crosshair gets bigger and smaller depending on if you are zoomed in?

    private Camera camera;

    private void Awake()
    {
        camera = transform.parent.GetComponent<Camera>();
    }

    private void Start()
    {
        timeNeededBetweenShots = 1.0f / fireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if(timeSinceLastShot >= timeNeededBetweenShots)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        timeSinceLastShot = 0.0f;

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, layer))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
