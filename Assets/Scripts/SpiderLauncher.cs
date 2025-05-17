using UnityEngine;

public class SpiderLauncher : MonoBehaviour
{
    public float launchAngle = 45f;
    public bool launched = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }
    }

    public void Launch(float launchForce)
    {
        if (launched) return;
        launched = true;

        float radians = launchAngle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        rb.AddForce(direction * launchForce, ForceMode.Impulse);
        GetComponent<DistanceTracker>()?.StartTracking();
    }

    public void ResetPosition()
    {
        launched = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
    }
}
