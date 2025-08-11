using System;
using UnityEngine;
using VehiclePhysics;

public class LaneSpeedLimitChecker : MonoBehaviour
{
    public static event Action OnWrongSpeed;
    public static event Action OnCorrectSpeed;

    public int speedLimit = int.MaxValue;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent)
        {
            VPVehicleToolkit carInfo = other.transform.parent.gameObject.GetComponent<VPVehicleToolkit>();

            if (carInfo != null && other.attachedRigidbody && other.CompareTag("Player"))
            {
                if (carInfo.speedInKph > speedLimit)
                {
                    OnWrongSpeed?.Invoke();
                }
                else
                {
                    OnCorrectSpeed?.Invoke();
                }
            }
        }
    }
}

