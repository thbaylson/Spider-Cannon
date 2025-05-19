using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;

public class SpiderLauncher : MonoBehaviour
{
    public bool launched = false;

    private Rigidbody rb;
    [SerializeField] private Rigidbody[] ragdollRbs;
    [SerializeField] private Collider[] ragdollColliders;

    private ChargeBar chargeBar;
    private AngleArrow angleArrow;

    public AudioClip yeetClip;
    public float yeetPitch;
    public AudioSource audioSource;
    public event Action OnLaunched;

    private void Awake()
    {
        chargeBar = GetComponent<ChargeBar>();
        OnLaunched += chargeBar.HandleLaunch;

        angleArrow = GetComponent<AngleArrow>();
        OnLaunched += angleArrow.HandleLaunch;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        // Disable controls if the player is launched.
        if (launched) return;

        // Move the launch arrow up and down.
        if (Input.GetKey(KeyCode.W))
        {
            angleArrow.MoveArrowUp();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            angleArrow.MoveArrowDown();
        }
        angleArrow.UpdateArrowSprite(chargeBar.GetCurrentCharge());

        // While the launch key is held down, charge the bar.
        if (!launched && Input.GetKey(KeyCode.Space))
        {
            chargeBar.Charge();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Launch(chargeBar.GetLaunchForce(), angleArrow.GetLaunchAngle());
        }
    }

    private void Launch(float launchForce, float launchAngle)
    {
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
