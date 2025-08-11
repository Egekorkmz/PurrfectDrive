using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Last5Sessions : MonoBehaviour
{
    public SessionControler sessionControler;
    public Button[] sessionButtons;

    public GameObject selectSessionCanvas;
    public GameObject sessionDetailsCanvas;
    public TextMeshProUGUI sessionDetailsText;
    public TextMeshProUGUI sessionDetailsTitle;

    public void crateLast5Sessions()
    {
        // Get the last sessions from the sessionControler
        List<Session> lastSessions = sessionControler.FindSessionsByUserName(PlayerPrefs.GetString("username"));

        // Reverse the list
        lastSessions.Reverse();

        // Loop through the sessions and set the button text
        int index = 0;
        foreach (var session in lastSessions)
        {
            sessionButtons[index].GetComponentInChildren<TMP_Text>().text = session.startTimeAsString;
            sessionButtons[index].gameObject.SetActive(true);
            int capturedIndex = index; // Capture the current index for the button listener  
            sessionButtons[capturedIndex].onClick.AddListener(() => OnSessionButtonClicked(session));
            index++;
            if (index >= sessionButtons.Length)
            {
                break;
            }
        }
    }

    public void setDeactiveButtons()
    {
        for (int i = 0; i < 5; i++)
        {
            sessionButtons[i].gameObject.SetActive(false);
        }
    }

    private void OnSessionButtonClicked(Session session)
    {
        if (session.mapName == "ExamMap")
        {
            string[] strings = session.examDetails();
            sessionDetailsTitle.text = strings[0];
            sessionDetailsText.text = strings[1];
        } else
        {
            sessionDetailsTitle.text = "Drive Summary";
            sessionDetailsText.text = session.sessionDetails();
        }
        selectSessionCanvas.SetActive(false);
        sessionDetailsCanvas.SetActive(true);


        EventSystem.current.SetSelectedGameObject(null);

        Button buttonToSelect = sessionDetailsCanvas.GetComponentInChildren<Button>();

        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject); //selection the button you dropped in the inspector
    }
}
