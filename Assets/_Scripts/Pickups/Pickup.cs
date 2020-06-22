using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] protected AudioClip pickupSpawnSoundEffect;
    [SerializeField] protected AudioClip pickupCollectedSoundEffect;

    [Header("Particle Effects")]
    [SerializeField] protected ParticleSystem pickupParticleEffect;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = pickupSpawnSoundEffect;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupAction();

            audioSource.clip = pickupCollectedSoundEffect;
            audioSource.Play();

            pickupParticleEffect.Play();

            GetComponent<MeshRenderer>().enabled = false;

            gameObject.GetComponentInChildren<Light>().enabled = false;

            Destroy(this.gameObject, 3.0f);
        }
    }

    protected virtual void PickupAction() { }

}
