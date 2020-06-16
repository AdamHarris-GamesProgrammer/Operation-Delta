using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected Transform goal;

    [Header("Miscellaneous Settings")]
    [SerializeField] protected EnemyStats stats;
    [SerializeField] private Transform eyes;
    [SerializeField] private LayerMask layer;


    [Header("Knockback Settings")]
    [SerializeField] protected float knockbackForce = 25.0f;

    protected float health;

    protected float attackTimer = 0.0f;

    protected bool isDead = false;

    protected NavMeshAgent agent;

    bool knockback = false;
    Vector3 direction;

    private AudioSource audioSource;

    private Animator animator;

    protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        agent.speed = stats.movementSpeed;
        health = stats.health;

        audioSource.clip = stats.spawnSound;
        audioSource.Play();
    }

    // Update is called once per frame
    protected void Update()
    {
        //Debug.Log("Enemy Update");

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward, Color.yellow);

        attackTimer += Time.deltaTime;

        if (health <= 0.0f)
        {
            isDead = true;
        }

        if (isDead)
        {
            OnDeath();
        }

        if (knockback)
        {
            agent.velocity = direction * knockbackForce;
        }
    }

    protected void OnDeath()
    {
        audioSource.clip = stats.deathSound;
        audioSource.Play();

        ScoreTextController.instance.ScoreUp(stats.scoreValue);

        Destroy(this.gameObject);
    }

    protected void Attack()
    {
        attackTimer = 0.0f;

        animator.SetTrigger("attack");
        RaycastHit hit;

        audioSource.clip = stats.attackSound;
        audioSource.Play();

        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit, stats.attackRange, layer))
        {
            PlayerController.instance.TakeDamage(stats.damage * stats.damageMultiplier);
        }
    }

    public void TakeDamage(float damageIn)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = stats.damagedSound;
            audioSource.Play();

            direction = PlayerController.instance.go.transform.forward.normalized;
            StartCoroutine("Knockback");

        }

        health -= damageIn;
    }

    protected IEnumerator Knockback()
    {
        knockback = true;

        agent.updateRotation = false;
        agent.speed = stats.movementSpeed * stats.damagedSpeedFactor;

        yield return new WaitForSeconds(stats.knockbackTime); 

        agent.updateRotation = true;
        agent.speed = stats.movementSpeed;

        knockback = false;
    }
}
