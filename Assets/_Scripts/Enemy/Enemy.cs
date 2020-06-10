using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform goal;

    [SerializeField] private EnemyStats stats;

    private float health;

    bool isDead = false;

    NavMeshAgent agent;


    private Animator animator;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        goal = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
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
        if (!isDead)
        {
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

                if(agent.remainingDistance <= stats.attackRange)
                {
                    animator.SetTrigger("attack");
                }
 

                //Debug.Log("Anim Playing: " + anim.isPlaying);


                agent.destination = goal.position;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float damageIn)
    {
        health -= damageIn;
        //TODO: Play damage sound
    }
}
