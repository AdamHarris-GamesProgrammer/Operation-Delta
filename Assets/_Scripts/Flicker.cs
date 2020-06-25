using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    [SerializeField] private float minTimeOn = 1.0f;
    [SerializeField] private float maxTimeOn = 1.0f;
    [SerializeField] private int flickersInCycle = 5;

    [SerializeField] private float minTimePerFlicker = 0.4f;
    [SerializeField] private float maxTimePerFlicker = 0.4f;

    private Light light;

    bool isFlickering;

    float timeBetweenFlickers;

    float flickerTimer;
    float flickerDuration;
    float betweenFlickerTimer;

    int flickersLeftInCycle;

    float defaultIntensity;

    void Awake()
    {
        light = GetComponent<Light>();

        flickersLeftInCycle = flickersInCycle;
        defaultIntensity = light.intensity;
        timeBetweenFlickers = UnityEngine.Random.Range(minTimeOn, maxTimeOn);
        flickerDuration = UnityEngine.Random.Range(minTimePerFlicker, maxTimePerFlicker);
    }

    void Update()
    {
        if (!isFlickering)
        {
            flickerTimer += Time.deltaTime;

            light.enabled = true;
            //light.intensity = defaultIntensity;

            if(flickerTimer >= timeBetweenFlickers)
            {
                isFlickering = true;
                flickerTimer = 0.0f;
            }
        }
        else
        {
            if(flickersLeftInCycle > 0)
            {
                betweenFlickerTimer += Time.deltaTime;
                if(betweenFlickerTimer <= flickerDuration)
                {
                    light.enabled = false;
                    //light.intensity = 0.1f;
                }
                else
                {
                    light.enabled = true;
                    //light.intensity = defaultIntensity;
                    flickersLeftInCycle--;
                    betweenFlickerTimer = 0.0f;

                }
            }
            else
            {
                isFlickering = false;
                flickersLeftInCycle = flickersInCycle;
                betweenFlickerTimer = 0.0f;
                timeBetweenFlickers = UnityEngine.Random.Range(minTimeOn, maxTimeOn);
                flickerDuration = UnityEngine.Random.Range(minTimePerFlicker, maxTimePerFlicker);
            }
        }
    }
}
