using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform goal;

    [SerializeField] private EnemyStats stats;
    [SerializeField] private Transform eyes;
    [SerializeField] private LayerMask layer;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private float knockbackForce = 25.0f;

    private float health;

    private float attackTimer = 0.0f;

    bool isDead = false;

    NavMeshAgent agent;

    bool knockback = false;
    Vector3 direction;

    private AudioSource audioSource;

    private Animator animator;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.speed = stats.movementSpeed;
        health = stats.health;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if (!isDead)
        {
            if (knockback)
            {
                agent.velocity = direction * knockbackForce;
            }


            if (health <= 0.0f)
            {
                isDead = true;
            }

            if (PlayerController.instance.isAlive)
            {
                agent.speed = stats.movementSpeed;

                if (agent.remainingDistance > 10.0f && stats.canSprint)
                {
                    //Debug.Log("Sprinting");
                    agent.speed = stats.movementSpeed * stats.sprintingSpeedFactor;
                }

                if (agent.remainingDistance <= stats.attackRange && attackTimer >= stats.attackRate)
                {
                    Attack();
                }
                //Debug.Log("Anim Playing: " + anim.isPlaying);

                agent.destination = goal.position;
            }
        }
        else
        {
            //TODO: Enemy Death sounds

            ScoreTextController.instance.ScoreUp(stats.scoreValue);

            Destroy(this.gameObject);
        }
    }

    void Attack()
    {
        attackTimer = 0.0f;

        animator.SetTrigger("attack");
        RaycastHit hit;

        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit, stats.attackRange, layer))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                PlayerController.instance.TakeDamage(stats.damage * stats.damageMultiplier);
            }
        }
    }

    public void TakeDamage(float damageIn, AudioClip desiredSound)
    {
        Debug.Log("Take Damage Called");

        if (!audioSource.isPlaying)
        {
            if(desiredSound != null)
            {
                audioSource.clip = desiredSound;
            }
            audioSource.Play();
            Debug.Log("Played");

            direction = PlayerController.instance.go.transform.forward.normalized;
            StartCoroutine("Knockback");

        }


        health -= damageIn;
    }

    IEnumerator Knockback()
    {
        knockback = true;

        agent.updateRotation = false;

        //Change nav mesh variables

        yield return new WaitForSeconds(0.2f); //TODO: Replace with field

        agent.updateRotation = true;

        knockback = false;
    }
}
