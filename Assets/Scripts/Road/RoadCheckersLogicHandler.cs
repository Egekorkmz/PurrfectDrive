using System;
using UnityEngine;
using static ViolationTypes;
using static LaneMarkingsCrossChecker;

public class RoadCheckersLogicHandler : MonoBehaviour
{
    //subscribe to these violation actions to handle related outcomes such as popup message and saving the violation
    public static event Action<Violation> OnWarningMessagePopup;

    private float laneDirectionTimer;
    private float laneSpeedTimer;
    private int lineCrossCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LaneDirectionChecker.OnWrongDirection += LaneWrongDirectionEventHandler;
        LaneDirectionChecker.OnCorrectDirection += LaneCorrectDirectionEventHandler;

        LaneSpeedLimitChecker.OnWrongSpeed += LaneWrongSpeedEventHandler;
        LaneSpeedLimitChecker.OnCorrectSpeed += LaneCorrectSpeedEventHandler;

        LaneMarkingsCrossChecker.OnLineCrossed += LineCrossedEventHandler;
    }

    private void OnDestroy()
    {
        LaneDirectionChecker.OnWrongDirection -= LaneWrongDirectionEventHandler;
        LaneDirectionChecker.OnCorrectDirection -= LaneCorrectDirectionEventHandler;

        LaneSpeedLimitChecker.OnWrongSpeed -= LaneWrongSpeedEventHandler;
        LaneSpeedLimitChecker.OnCorrectSpeed -= LaneCorrectSpeedEventHandler;

        LaneMarkingsCrossChecker.OnLineCrossed -= LineCrossedEventHandler;
    }

    //if player enter back to the correct line, the timer resets
    private void LaneCorrectDirectionEventHandler()
    {
        laneDirectionTimer = 0;
    }

    void LaneWrongDirectionEventHandler()
    {
        laneDirectionTimer += Time.deltaTime;
        //3 seconds, called once
        if (Mathf.Floor(laneDirectionTimer) == 3)
        {
            //↩️ Wrong turn detected. Please follow the road signs.
            OnWarningMessagePopup?.Invoke(Violation.GetViolation(ViolationCode.wrongDirection));
            laneDirectionTimer += 1; //to stop from calling again
        }
    }

    //if speed drops down to speed limit, timer resets
    void LaneCorrectSpeedEventHandler()
    {
        laneSpeedTimer = 0;
    }

    void LaneWrongSpeedEventHandler()
    {
        laneSpeedTimer += Time.deltaTime;
        //2 seconds, called once
        if (Mathf.Floor(laneSpeedTimer) == 2)
        {
            OnWarningMessagePopup?.Invoke(Violation.GetViolation(ViolationCode.exceededSpeed));
            laneSpeedTimer += 1; //to stop from calling again
        }
    }

    void LineCrossedEventHandler(LineType lineType)
    {
        lineCrossCounter += 1;
        if (lineCrossCounter == 15)
        {
            OnWarningMessagePopup?.Invoke(Violation.GetViolation(ViolationCode.crossLane));
            lineCrossCounter = 0;
        }
    }
}
