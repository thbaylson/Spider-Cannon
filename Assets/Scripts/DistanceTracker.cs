using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private float StopSpeed = 0.01f;
    [SerializeField] public float DistanceTraveled { get; private set; } = 0f;

    // We need a stop delay or else we will end the run on the first frame after we start tracking.
    [SerializeField] private float StopDelay = 1f;
    [SerializeField] private float CoroutineTimer = 0.1f;
    private float stoppingTimer = 0f;

    [SerializeField] public CinemachineVirtualCamera followCam;
    [SerializeField] public float originalFollowCamFOV;
    [SerializeField] public float originalCamDistance;
    [SerializeField] private float zoomedOutCamDistance;
    [SerializeField] private float followCamZoomOutPOV;

    private Rigidbody rb;
    private float startX;
    private bool isTracking = false;

    public event Action OnStopped;

    private SpiderLauncher _spiderLauncher;    

    private void Awake()
    {
        _spiderLauncher = GetComponent<SpiderLauncher>();
        _spiderLauncher.OnLaunched += StartTracking;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startX = transform.position.x;
    }

    private IEnumerator Track()
    {
        // Small buffer to make sure we're moving before we check velocity.
        yield return new WaitForSeconds(.1f);

        while (isTracking)
        {
            DistanceTraveled = transform.position.x - startX;

            // TODO: What if we used a raycast to determine how far we are from the ground?
            if (rb.velocity.magnitude < StopSpeed)
            {
                // Once we start stopping, we want to stop the player from moving.
                // Except this doesn't actually work the way I thought it would...
                rb.velocity = Vector3.zero;

                followCam.m_Lens.FieldOfView = originalFollowCamFOV;
                followCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = originalCamDistance;
                stoppingTimer += CoroutineTimer + Time.deltaTime;
                if (stoppingTimer >= StopDelay)
                {
                    isTracking = false;
                    OnStopped?.Invoke();
                }
            }

            yield return new WaitForSeconds(CoroutineTimer);
        }
    }

    public void StartTracking()
    {
        isTracking = true;
        followCam.m_Lens.FieldOfView = followCamZoomOutPOV;
        followCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = zoomedOutCamDistance;

        StartCoroutine(Track());
    }
}
