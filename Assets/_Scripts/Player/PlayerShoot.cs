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
    [SerializeField] private float bulletDamage = 15.0f;

    [Header("Reload Settings")]
    private float reloadDuration = 2.5f;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip dryShotSound;
    private AudioSource audioSource;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem barrelSmokeParticles;
    [SerializeField] private ParticleSystem traceParticles;


    private Animation anim;

    float reloadTimer;

    float timeSinceLastShot;
    float timeNeededBetweenShots;
    int currentClip;

    bool reloading;

    //Recoil
    //auto reload when bullets are out
    //optional reload by pressing r
    //fire effects from gun
    //Crosshair gets bigger and smaller depending on if you are zoomed in?

    private Camera camera;

    private void Awake()
    {
        camera = transform.GetComponentInParent<Camera>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
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
        anim.Play("Recoil");

        audioSource.clip = shootSound;
        audioSource.Play();

        barrelSmokeParticles.Play();
        traceParticles.Play();

        timeSinceLastShot = 0.0f;
        currentClip--;

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, layer))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                hit.transform.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
            }
        }

        if(currentClip == 0)
        {
            reloadTimer = reloadDuration;
            reloading = true;
        }
    }
}
