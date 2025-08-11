using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem.LowLevel;

public class TrafficLightController : MonoBehaviour
{
    public static event Action<String> OnWarningMessagePopup;

    public TrafficLightState currentState;
    
    public Light redLight;
    public Light yellowLight;
    public Light greenLight;

    public float redBufferDuration= 1.5f;
    public float yellowDuration = 1.5f;
    public float greenDuration = 7f;

    void Start()
    {
        redLight.enabled = true;
        yellowLight.enabled = false;
        greenLight.enabled = false;
        
    }

    public IEnumerator TrafficLightCycle()
    {

            SetLightState(TrafficLightState.YellowStart);
            yield return new WaitForSeconds(yellowDuration);

            // Green light
            SetLightState(TrafficLightState.Green);
            yield return new WaitForSeconds(greenDuration);

            // Yellow light
            SetLightState(TrafficLightState.Yellow);
            yield return new WaitForSeconds(yellowDuration);

            // Red light
            SetLightState(TrafficLightState.Red);
            yield return new WaitForSeconds(redBufferDuration);

    }

    private void SetLightState(TrafficLightState newState)
    {
        currentState = newState;

        redLight.enabled = (newState == TrafficLightState.Red || newState == TrafficLightState.YellowStart);
        yellowLight.enabled = (newState == TrafficLightState.Yellow || newState == TrafficLightState.YellowStart);
        greenLight.enabled = (newState == TrafficLightState.Green);
    }
}
