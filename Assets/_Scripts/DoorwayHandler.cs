using UnityEngine;

public class DoorwayHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " has entered the trigger");

        Crawler crawlerComponent = other.gameObject.GetComponent<Crawler>();
        if (crawlerComponent)
        {
            crawlerComponent.OnCrawl();
        }

        PlayerMovement playerComponent = other.gameObject.GetComponent<PlayerMovement>();
        if (playerComponent)
        {
            playerComponent.canStand = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name + " has left the trigger");

        Crawler crawlerComponent = other.gameObject.GetComponent<Crawler>();
        if (crawlerComponent)
        {
            crawlerComponent.OnCrawlExit();
        }

        PlayerMovement playerComponent = other.gameObject.GetComponent<PlayerMovement>();
        if (playerComponent)
        {
            playerComponent.canStand = true;
        }
    }
}
