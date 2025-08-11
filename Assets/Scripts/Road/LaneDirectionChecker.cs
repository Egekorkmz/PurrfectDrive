using System;
using UnityEngine;
using VehiclePhysics;

public class LaneDirectionChecker : MonoBehaviour
{
    public static event Action OnWrongDirection; 
    public static event Action OnCorrectDirection;

    //Lane
    public enum LineOrienatation
    {
        Left,
        Right
    }

    //the line orientation should be determined according to the road parent object's forward direction
    public LineOrienatation lineOrienatation;

    private float currAngle;
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent)
        {
            VPVehicleToolkit carInfo = other.transform.parent.gameObject.GetComponent<VPVehicleToolkit>();

            if (carInfo != null && other.attachedRigidbody && other.CompareTag("Player"))
            {
                currAngle = Vector3.Angle(gameObject.transform.forward, other.transform.forward);

                // if the car is going in wrong direction or going reverse in wrong direction
                switch (lineOrienatation)
                {
                    case LineOrienatation.Left:
                        if (currAngle > 270 || currAngle < 90 || (currAngle < 270 && currAngle > 90 && carInfo.speedInKph < 0))
                        {
                            OnWrongDirection?.Invoke();
                        }
                        else
                        {
                            OnCorrectDirection?.Invoke();
                        }
                        break;

                    case LineOrienatation.Right:
                        if ((currAngle < 270 && currAngle > 90) || ((currAngle > 270 || currAngle < 90) && carInfo.speedInKph < 0))
                        {
                            OnWrongDirection?.Invoke();
                        }
                        else
                        {
                            OnCorrectDirection?.Invoke();
                        }
                        break;
                }
            }
        }
    }
}
