using System;
using TMPro;
using UnityEngine;
using static ViolationTypes;

public class ViolationEventHandler : MonoBehaviour
{
    public GameObject warningPopup; // Assign your Canvas here
    public TextMeshProUGUI warningPopupText;

    public TextMeshProUGUI warningListText;
    public GameObject warningList;

    public SessionControler sessionControler;

    public static event Action examFailed;

    private float timer = 0f;
    private bool isShowing = false;
    public float showDuration = 3f;

    void Start()
    {
        sessionControler.InitializeSession();
        RoadCheckersLogicHandler.OnWarningMessagePopup += OnWarningMessagePopup;
        TurnSignalDetector.OnWarningMessagePopup += OnWarningMessagePopup;
        crashDetector.OnWarningMessagePopup += OnWarningMessagePopup;
        TrafficLightTrigger.OnWarningMessagePopup += OnWarningMessagePopup;
    }

    private void OnDestroy()
    {
        RoadCheckersLogicHandler.OnWarningMessagePopup -= OnWarningMessagePopup;
        TurnSignalDetector.OnWarningMessagePopup -= OnWarningMessagePopup;
        crashDetector.OnWarningMessagePopup -= OnWarningMessagePopup;
        TrafficLightTrigger.OnWarningMessagePopup -= OnWarningMessagePopup;
    }

    private void Update()
    {
        if (isShowing)
        {
            timer += Time.deltaTime;
            if (timer >= showDuration)
            {
                warningPopup.SetActive(false);
                warningList.SetActive(true);
                isShowing = false;
                timer = 0f;
            }
        }
    }

    public void showWarningPopup(string message)
    {
        warningPopupText.text = message;
        warningPopup.SetActive(true);

        warningList.SetActive(false);

        isShowing = true;
        timer = 0f;
    }

    public void OnWarningMessagePopup(Violation violation)
    {
        Debug.Log("[Warning]: " + violation.Message);
        bool examEnd = sessionControler.session.addError(violation);

        if (examEnd)
        {
            examFailed?.Invoke();
        }

        warningListText.text = sessionControler.session.GetErrorList();

        showWarningPopup(violation.Message);
    }
}
