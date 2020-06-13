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

    public float healthAmount = 100.0f;
    private float health;

    public bool canBeDamaged = false;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();
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
            
        }
    }

    void OnDeath()
    {
        isAlive = false;
        Debug.Log("Dead");
        GameManager.instance.isGameOver = true;

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
