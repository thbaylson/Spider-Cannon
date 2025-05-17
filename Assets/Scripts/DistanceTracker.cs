using System;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private float StopSpeed = 0.01f;
    [SerializeField] private float DistanceTraveled = 0f;

    // We need a stop delay or else we will end the run on the first frame after we start tracking.
    [SerializeField] private float StopDelay = 1f;
    [SerializeField] public float originalFollowCamFOV;
    private float stoppingTimer = 0f;

    private Rigidbody rb;
    private float startX;
    private bool isTracking = false;

    public event Action<float> OnDistanceChanged;
    public event Action<float> OnRunEnded;

    private SpiderLauncher _spiderLauncher;
    

    private void Awake()
    {
        _spiderLauncher = GetComponent<SpiderLauncher>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isTracking) return;

        DistanceTraveled = transform.position.x - startX;
        OnDistanceChanged?.Invoke(DistanceTraveled);
        if (rb.velocity.magnitude < StopSpeed)
        {
            _spiderLauncher.followCam.m_Lens.FieldOfView = originalFollowCamFOV;
            stoppingTimer += Time.deltaTime;
            if (stoppingTimer >= StopDelay)
            {
                isTracking = false;
                OnRunEnded?.Invoke(DistanceTraveled);
            }
        }
    }

    public void StartTracking()
    {
        startX = transform.position.x;
        isTracking = true;
    }
}
