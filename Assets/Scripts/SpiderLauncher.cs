using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiderLauncher : MonoBehaviour
{
    public bool launched = false;
    public int JumpsLeft = 1;
    public int MaxJumps = 1;
    [SerializeField] TMP_Text jumpsLeftText;
    public bool canCharge = false;

    private Rigidbody rb;
    [SerializeField] private Rigidbody[] ragdollRbs;
    [SerializeField] private Collider[] ragdollColliders;

    private ChargeBar chargeBar;
    private AngleArrow angleArrow;
    private DistanceTracker distanceTracker;

    public AudioClip yeetClip;
    public float yeetPitch;
    public AudioSource audioSource;

    public event Action OnLaunched;
    public event Action<float> OnRunEnded;

    private void Awake()
    {
        chargeBar = GetComponent<ChargeBar>();
        OnLaunched += chargeBar.HandleLaunch;

        angleArrow = GetComponent<AngleArrow>();
        OnLaunched += angleArrow.HandleLaunch;

        distanceTracker = GetComponent<DistanceTracker>();
        distanceTracker.OnStopped += HandleStopped;

        UpdateJumpsLeftText();
    }

    private void HandleStopped()
    {
        if (JumpsLeft <= 0)
        {
            OnRunEnded?.Invoke(distanceTracker.DistanceTraveled);
        }
        else
        {
            // Reset the spider
            launched = false;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            ToggleRagdoll(false);

            chargeBar.ShowUI();
            angleArrow.ShowUI();
        }
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
        if (!launched && canCharge && Input.GetKey(KeyCode.Space))
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
        JumpsLeft--;
        UpdateJumpsLeftText();

        // Invoke subscriber event.
        OnLaunched?.Invoke();
        
        // Calculate the force vector.
        float radians = launchAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        rb.isKinematic = false;
        rb.AddForce(direction * launchForce, ForceMode.Impulse);
        YeetUponLaunch();
        // Ragdoll.
        ToggleRagdoll(true);
    }

    private void ToggleRagdoll(bool toggleOn)
    {
        foreach (var rb in ragdollRbs)
        {
            rb.isKinematic = !toggleOn;
            rb.detectCollisions = toggleOn;
        }
        foreach (var col in ragdollColliders)
        {
            col.enabled = toggleOn;
        }
    }
    public void YeetUponLaunch()
    {
        audioSource.pitch = yeetPitch;
        audioSource.PlayOneShot(yeetClip, 1);
    }

    public void ConsumeAllJumps()
    {
        JumpsLeft = 0;
        UpdateJumpsLeftText();
    }

    private void UpdateJumpsLeftText()
    {
        jumpsLeftText.text = $"Jumps Left: {JumpsLeft}/{MaxJumps}";
    }
}
