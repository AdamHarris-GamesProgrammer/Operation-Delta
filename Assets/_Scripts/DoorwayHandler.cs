using UnityEngine;

public class DoorwayHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " has entered the trigger");

        Crawler crawlerComponent = other.gameObject.GetComponent<Crawler>();
        if (crawlerComponent)
        {
            Debug.Log("Crawler Component Found");
            crawlerComponent.OnCrawl();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name + " has left the trigger");

        Crawler crawlerComponent = other.gameObject.GetComponent<Crawler>();
        if (crawlerComponent)
        {
            Debug.Log("Crawler Component Found");
            crawlerComponent.OnCrawlExit();
        }
    }
}
