using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
public class SpiderImpactAudio : MonoBehaviour
{
    public AudioClip bounceClip;
    public AudioClip[] owSounds;
    public ParticleSystem impactVFX;
    public float minImpactVelocity = 1f;
    public float maxImpactVelocity = 10f;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;
     public float bounceCooldown;
    private float lastBounceTime;
    public ShakeData impactShakeData;
    private AudioSource audioSource;

    void Awake()
    {

        impactVFX = impactVFX.GetComponent<ParticleSystem>();
        audioSource = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastBounceTime > bounceCooldown)
        {
            float impactForce = collision.relativeVelocity.magnitude;
            if (impactForce >= minImpactVelocity)
            {
                float pitch = Mathf.Lerp(minPitch, maxPitch, impactForce / maxImpactVelocity);
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(bounceClip, 1);
                PlayHurtSound();
                impactForce = 0;
                PlayCamShake();
            }
            lastBounceTime = Time.time;
        }
    }
    
    public void PlayHurtSound()
    {
        float ouchPitch = Random.Range(1.5f, 2.2f);
        audioSource.pitch = ouchPitch;
        int randOwSound = Random.Range(0, owSounds.Length);
        audioSource.PlayOneShot(owSounds[randOwSound], .25f);
    }
     public void PlayCamShake()
    {
        impactVFX.Play();
        CameraShakerHandler.Shake(impactShakeData);
    }
}
