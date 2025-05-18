using UnityEngine;

public class SuperBounce : MonoBehaviour
{
    [SerializeField] private float extraImpulse = 100f;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            var rb = col.gameObject.GetComponent<Rigidbody>();
            if (rb == null) return;

            rb.AddForce((2 * Vector3.up + Vector3.right) * extraImpulse, ForceMode.Impulse);
        }
    }
}
