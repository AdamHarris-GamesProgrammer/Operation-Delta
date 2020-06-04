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
    [SerializeField] private int magazineSize = 24;
    [Header("Reload Settings")]
    private float reloadDuration = 2.5f;

    float reloadTimer;

    float timeSinceLastShot;
    float timeNeededBetweenShots;
    int currentClip;

    bool reloading;

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
        currentClip = magazineSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (reloading)
        {
            reloadTimer -= Time.deltaTime;
            Debug.Log("Time until reload: " + reloadTimer);
            if (reloadTimer <= 0.0f)
            {
                reloading = false;
                currentClip = magazineSize;
                Debug.Log("Reload Complete");
            }
        }

        timeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if(timeSinceLastShot >= timeNeededBetweenShots && !reloading)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        timeSinceLastShot = 0.0f;
        currentClip--;
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, layer))
        {
            Debug.Log(hit.transform.name);
        }

        if(currentClip == 0)
        {
            reloadTimer = reloadDuration;
            reloading = true;
        }
    }
}
