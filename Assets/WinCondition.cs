using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DistanceTracker dt = FindAnyObjectByType<DistanceTracker>();
        dt.reachedFinish = true;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Win Condition Triggered.");
            Rigidbody[] rbList= other.gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rbList)
            {
                rb.isKinematic = true;
            }
        }
    }
}
