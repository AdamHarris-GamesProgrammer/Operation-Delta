using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform goal;

    [Header("Miscellaneous Settings")]
    [SerializeField] private EnemyStats stats;
    [SerializeField] private Transform eyes;
    [SerializeField] private LayerMask layer;


    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 25.0f;

    private float health;

    private float attackTimer = 0.0f;

    [SerializeField] private int crawlMask;

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

        audioSource.clip = stats.spawnSound;
        audioSource.Play();
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

            if(agent.areaMask == 3)
            {
                Debug.Log("Crawling");
                agent.speed = stats.movementSpeed * stats.crawlingSpeedFactor;
            }

            

            //Debug.Log(NavMesh.GetAreaFromName("Crawlable"));
            Debug.Log(agent.areaMask);

            if (health <= 0.0f)
            {
                isDead = true;
            }

            if (PlayerController.instance.isAlive)
            {
                agent.speed = stats.movementSpeed;

                if (agent.remainingDistance > 10.0f && stats.canSprint && agent.areaMask != NavMesh.GetAreaFromName("Crawlable"))
                {
                    //Debug.Log("Sprinting");
                    agent.speed = stats.movementSpeed * stats.sprintingSpeedFactor;
                }

                if (agent.remainingDistance <= stats.attackRange && attackTimer >= stats.attackRate)
                {
                    Attack();
                }

                agent.destination = goal.position;
            }
        }
        else
        {

            audioSource.clip = stats.deathSound;
            audioSource.Play();

            ScoreTextController.instance.ScoreUp(stats.scoreValue);

            Destroy(this.gameObject);
        }
    }

    void Attack()
    {
        attackTimer = 0.0f;

        animator.SetTrigger("attack");
        RaycastHit hit;

        audioSource.clip = stats.attackSound;
        audioSource.Play();

        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward, out hit, stats.attackRange, layer))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                PlayerController.instance.TakeDamage(stats.damage * stats.damageMultiplier);
            }
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

    IEnumerator Knockback()
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
