using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [Header("Layer Settings")]
    [SerializeField] private LayerMask layer;

    [Header("Gun Settings")]
    [SerializeField] private float range = 100.0f;
    [SerializeField] private int totalBullets = 240;
    [SerializeField] private float bulletDamage = 15.0f;
    public float damageFactor = 1.0f;

    [Tooltip("Amount of bullets fired per second")]
    [SerializeField] private float fireRate = 5.0f;
    [SerializeField] private int magazineSize = 24;

    [Header("Reload Settings")]
    private float reloadDuration = 2.5f;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip dryShotSound;
    private AudioSource audioSource;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem barrelSmokeParticles;
    [SerializeField] private ParticleSystem traceParticles;

    [Header("UI Settings")]
    [SerializeField] private Image clipImage;
    [SerializeField] private Text totalAmmoLeftText;

    private Animation anim;

    float reloadTimer;

    float timeSinceLastShot;
    float timeNeededBetweenShots;
    int currentClip;

    bool reloading;

    //auto reload when bullets are out
    //optional reload by pressing r
    //fire effects from gun
    //Crosshair gets bigger and smaller depending on if you are zoomed in?

    private Camera camera;


    float effectTimer;
    bool effectActive = false;

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

        totalAmmoLeftText.text = totalBullets.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerController.instance.isAlive)
        {
            if (reloading)
            {
                reloadTimer += Time.deltaTime;

                float fill = reloadTimer / reloadDuration;
                clipImage.fillAmount = fill;

                if (reloadTimer >= reloadDuration)
                {
                    reloading = false;
                    currentClip = magazineSize;
                    clipImage.fillAmount = 1.0f;
                }
            }

            if (effectActive)
            {
                effectTimer -= Time.deltaTime;
                if(effectTimer <= 0.0f)
                {
                    damageFactor = 1.0f;
                    effectActive = false;
                }
            }

            timeSinceLastShot += Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                if (timeSinceLastShot >= timeNeededBetweenShots && !reloading && totalBullets > 0)
                {
                    Shoot();
                }
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

        totalBullets--;
        totalAmmoLeftText.text = totalBullets.ToString();

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
            reloadTimer = 0.0f;
            reloading = true;
        }

        float fill = (float)currentClip / (float)magazineSize;

        clipImage.fillAmount = fill;
    }

    public void AddAmmo(int amount)
    {
        totalBullets += amount;
        totalAmmoLeftText.text = totalBullets.ToString();
    }

    public void SetEffectDuration(float inTimer)
    {
        effectTimer = inTimer;
        effectActive = true;
    }
}
