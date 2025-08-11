using System;
using UnityEngine;
using static ViolationTypes;

public class TrafficLightTrigger : MonoBehaviour
{
    public static event Action<Violation> OnWarningMessagePopup;

    public TrafficLightController trafficLightController;

    private float currAngle;

    private void OnTriggerEnter(Collider other)
    {
        currAngle = Vector3.Angle(gameObject.transform.forward, other.transform.forward);

        if (other.CompareTag("Player") && (currAngle > 330 || currAngle < 30))
        {
            if (trafficLightController.currentState == TrafficLightState.Red)
            {
                OnWarningMessagePopup.Invoke(Violation.GetViolation(ViolationCode.crossRedLight));
                //UIManager.Instance.ShowViolationPopup("You ran a red light!");
            }
        }
    }
}
