using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Enemy
{
    [HideInInspector] public bool isCrawling;

    [SerializeField] private float yScaleOffset = 0.5f;

    private void Awake()
    {
        base.Awake();
    }

    //+0.5 on stand -0.5 y offset on crouch

    void Update()
    {
        base.Update();

        //Debug.Log("Crawler Update");

        if (!isDead)
        {
            if (PlayerController.instance.isAlive)
            {
                agent.speed = stats.movementSpeed;

                if (agent.remainingDistance > 10.0f && stats.canSprint && !isCrawling)
                {
                    //Debug.Log("Sprinting");
                    agent.speed = stats.movementSpeed * stats.sprintingSpeedFactor;
                }
                else if (isCrawling)
                {
                    agent.speed = stats.movementSpeed * stats.crawlingSpeedFactor;
                }
                else
                {
                    agent.speed = stats.movementSpeed;
                }

                if (agent.remainingDistance <= stats.attackRange && attackTimer >= stats.attackRate)
                {
                    Attack();
                }

                agent.destination = goal.position;
            }
        }
    }

    public void OnCrawl()
    {
        isCrawling = true;
        Debug.Log("On Crawl");
        HeightCalculation(-yScaleOffset);
    }

    public void OnCrawlExit()
    {
        isCrawling = false;
        Debug.Log("On Crawl Exit");
        HeightCalculation(yScaleOffset);
    }

    void HeightCalculation(float yOffset)
    {
        Vector3 newScale = new Vector3(transform.localScale.x, transform.localScale.y + yOffset, transform.localScale.z);
        transform.localScale = newScale;
    }
}
