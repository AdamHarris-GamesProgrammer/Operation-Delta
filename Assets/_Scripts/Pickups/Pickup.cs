using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] protected AudioClip pickupSpawnSoundEffect;
    [SerializeField] protected AudioClip pickupCollectedSoundEffect;

    [Header("Particle Effects")]
    [SerializeField] protected ParticleSystem pickupParticleEffect;

    [Header("UI Settings")]
    private Text pickupText;
    [SerializeField] private string pickupName;
    [SerializeField] private Color textColor;
    [SerializeField] private float textDuration = 2.5f;

    private AudioSource audioSource;
    bool triggered;
    float timer;
    private void Awake()
    {
        pickupText = GameObject.FindGameObjectWithTag("PickupText").GetComponent<Text>();
        
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = pickupSpawnSoundEffect;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !triggered)
        {
            triggered = true;

            PickupAction();

            audioSource.clip = pickupCollectedSoundEffect;
            audioSource.Play();

            pickupParticleEffect.Play();


            pickupText.color = textColor;
            pickupText.text = pickupName;

            GameManager.instance.PickupActive();

            GetComponent<MeshRenderer>().enabled = false;

            gameObject.GetComponentInChildren<Light>().enabled = false;

            Destroy(this.gameObject, 3.0f);
        }
    }

    private void Update()
    {
        if (triggered)
        {
            textDuration -= Time.deltaTime;
            if(textDuration <= 0.0f)
            {
                GameManager.instance.PickupDisabled();
                pickupText.text = "";
            }
        }
    }

    protected virtual void PickupAction() { }

}
