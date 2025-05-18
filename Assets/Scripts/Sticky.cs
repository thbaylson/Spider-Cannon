using Unity.VisualScripting;
using UnityEngine;

public class Sticky : MonoBehaviour
{
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
    }
}
