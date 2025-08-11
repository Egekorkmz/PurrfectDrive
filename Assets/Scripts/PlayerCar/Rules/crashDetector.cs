using System;
using UnityEngine;
using static ViolationTypes;

public class crashDetector : MonoBehaviour
{
    public static event Action<Violation> OnWarningMessagePopup;
    public static event Action examFailed;
    public static event Action examFinished;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("environment"))
        {
            //⚠️ You scratched the vehicle! Drive more carefully.
            OnWarningMessagePopup?.Invoke(Violation.GetViolation(ViolationCode.crashEnvironment));
        }
        else if(collision.gameObject.CompareTag("npcHuman"))
        {
            //🚫 You hit a pedestrian! This is a critical violation.
            OnWarningMessagePopup?.Invoke(Violation.GetViolation(ViolationCode.crashNpcHuman));
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("examBoundary")) // Check for specific fail condition
        {
            examFailed?.Invoke();
        } else if (collision.gameObject.CompareTag("Finish"))
        {
            examFinished?.Invoke();
        }
    }
}
