using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;

public class SpiderLauncher : MonoBehaviour
{
    public float launchAngle = 0f;
    public bool launched = false;

    private Rigidbody rb;
    [SerializeField] private BoxCollider colliderToTurnOff; // after launch, we wanna turn off the box collider.
    [SerializeField] private Rigidbody[] ragdollRbs;
    [SerializeField] private Collider[] ragdollColliders;
    public AudioClip yeetClip;
    public float yeetPitch;
    public AudioSource audioSource;
    public event Action OnLaunched;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliderToTurnOff = gameObject.GetComponent<BoxCollider>();
        ragdollRbs = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        audioSource = GameObject.FindWithTag("SFX").GetComponent<AudioSource>();
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Launch(float launchForce)
    {
        if (launched) return;
        launched = true;

        // Invoke subscriber event.
        OnLaunched?.Invoke();
        
        // Calculate the force vector.
        float radians = launchAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        rb.isKinematic = false;
        rb.AddForce(direction * launchForce, ForceMode.Impulse);
        YeetUponLaunch();
        // Ragdoll.
        TurnOnRagdoll();
    }

    private void TurnOnRagdoll()
    {
        foreach (var rb in ragdollRbs)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
        foreach (var col in ragdollColliders)
        {
            col.enabled = true;
        }
    }
    public void YeetUponLaunch()
    {
        audioSource.pitch = yeetPitch;
        audioSource.PlayOneShot(yeetClip, 1);
    }
}
