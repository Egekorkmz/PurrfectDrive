using System;
using UnityEngine;
using VehiclePhysics;

public class LaneMarkingsCrossChecker : MonoBehaviour
{
    public static event Action<LineType> OnLineCrossed;
    public static event Action OnLineNotCrossed;

    //Lane
    public enum LineType
    {
        Dashed,
        Solid
    }

    //the line type should be determined according to road line markings
    public LineType lineType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent)
        {
            VPVehicleToolkit carInfo = other.transform.parent.gameObject.GetComponent<VPVehicleToolkit>();

            if (carInfo != null && other.attachedRigidbody && other.CompareTag("Player"))
            {
                OnLineCrossed?.Invoke(lineType);
            }
        }
    }
}
