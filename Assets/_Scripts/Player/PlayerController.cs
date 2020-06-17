using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [HideInInspector] public bool isAlive { get; set; }

    [Header("Game UI")]
    [SerializeField] private Image hitEffect;
    [SerializeField] private Image healthBar;


    [Header("Sounds")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip damagedSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Gameplay Settings")]
    [SerializeField] LayerMask doorLayer;

    public float healthAmount = 100.0f;
    private float health;

    public bool canBeDamaged = false;


    public GameObject go;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        //DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();

        go = this.gameObject;
    }

    private void Start()
    {
        isAlive = true;

        health = healthAmount;
    }

    private void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;

                Camera cam = go.GetComponentInChildren<Camera>();

                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1.5f, doorLayer))
                {
                    hit.collider.gameObject.GetComponent<Door>().Unlock();

                    GameManager.instance.DoorUnlocked();
                }

            }
        }
    }

    void OnDeath()
    {
        isAlive = false;
        Debug.Log("Dead");

        GameManager.instance.OnDeath();

        audioSource.clip = deathSound;
        audioSource.Play();
    }

    public void TakeDamage(float damageIn)
    {
        if (canBeDamaged)
        {
            health -= damageIn;

            if (health <= 0.0f)
            {
                OnDeath();
            }
        }
        StartCoroutine("HitEffect");

        audioSource.clip = damagedSound;
        audioSource.Play();

        //Calculate the percentage of health left;

        float fillAmount = health / healthAmount;

        healthBar.fillAmount = fillAmount;
    }

    IEnumerator HitEffect()
    {
        //Debug.Log("Hit Effect On");
        hitEffect.gameObject.SetActive(true);

        for (float ft = 1f; ft >= 0; ft -= 0.3f)
        {
            Color c = hitEffect.color;
            c.a = ft;
            hitEffect.color = c;
            yield return new WaitForSeconds(.1f);
        }

        //Debug.Log("Hit Effect Off");
        hitEffect.gameObject.SetActive(false);
    }
}
