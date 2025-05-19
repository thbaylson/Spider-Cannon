using Unity.VisualScripting;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    public AudioSource audioSFX;
    public AudioClip squishSound;
    private void Start()
    {
        audioSFX = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider col)
    {
        var comp = col.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in comp)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            rb.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        PlaySquishSound();
        col.gameObject.GetComponentInChildren<SpiderImpactAudio>().PlayCamShake();

        // Take away all remaining jumps
        var spiderLauncher = col.gameObject.GetComponent<SpiderLauncher>();
        if (spiderLauncher != null)
        {
            spiderLauncher.ConsumeAllJumps();
        }
    }
    void PlaySquishSound()
    {
        audioSFX.pitch = 1;
        audioSFX.PlayOneShot(squishSound, .5f);
    }
}
