using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI sessionDetails;
    public TextMeshProUGUI sessionDetailsTitle;

    public GameObject loginCanvas;
    public GameObject adminMenuCanvas;
    public GameObject mainMenu;
    public GameObject sessionDetailsCanvas;
    public GameObject hightlight;
    
    public UserHandler userHandler;
    public SessionControler sessionControler;

    //use these transfer user info between sessions
    public static bool isLoggedIn = false;
    public static string loggedInUser = null;

    private string adminUserName = "admin";
    private string correctPassword = "12345";

    void Start()
    {
        if (PlayerPrefs.GetString("comingFrom") == "session")
        {
            PlayerPrefs.DeleteKey("comingFrom");
            GotoSessionDetails();
        }
        else if (PlayerPrefs.GetString("comingFrom") == "training")
        {
            PlayerPrefs.DeleteKey("comingFrom");
            GotoMainMenu();
        }
        else if (PlayerPrefs.GetString("comingFrom") == "exam")
        {
            PlayerPrefs.DeleteKey("comingFrom");
            GotoMainMenu();
        } else if (PlayerPrefs.GetString("comingFrom") == "examFinish")
        {
            PlayerPrefs.DeleteKey("comingFrom");
            GotoSessionDetails();
        }

        loginButton.onClick.AddListener(ValidateLogin);
    }

    void ValidateLogin()
    {
        string enteredUsername = usernameInput.text;
        string enteredPassword = passwordInput.text;
        User user = userHandler.FindUser(enteredUsername);

        if (enteredUsername == adminUserName)
        {
            if (enteredPassword == "")
            {
                passwordInput.gameObject.SetActive(true);
                feedbackText.text = "Please enter password!";
                feedbackText.color = Color.red;

            } else if (enteredPassword == correctPassword)
            {
                feedbackText.text = "Login Successful!";
                feedbackText.color = Color.green;

                isLoggedIn = true;
                loggedInUser = enteredUsername;
                PlayerPrefs.SetString("username", "admin");
                Invoke("GotoAdminMenu", 1.5f);
            }
        }
        else if (user != null)
        {
            feedbackText.text = "Login Successful!";
            feedbackText.color = Color.green;
            isLoggedIn = true;
            loggedInUser = enteredUsername;
            PlayerPrefs.SetString("username", loggedInUser);
            Invoke("GotoMainMenu", 1.5f);
        }
        else
        {
            feedbackText.text = "Invalid Username!";
            feedbackText.color = Color.red;
            PlayerPrefs.SetString("username", "");
        }
    }

    void GotoMainMenu()
    {
        passwordInput.gameObject.SetActive(false);
        loginCanvas.SetActive(false);
        mainMenu.SetActive(true);

        Button buttonToSelect = mainMenu.GetComponentInChildren<Button>();

        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject); //selection the button you dropped in the inspector
    }

    public void LoginAsAGuest()
    {
        passwordInput.gameObject.SetActive(false);
        feedbackText.text = "Login Successful!";
        feedbackText.color = Color.green;
        isLoggedIn = true;
        loggedInUser = "guest_" + DateTime.Now.ToString("yyyyMMdd");
        PlayerPrefs.SetString("username", loggedInUser);

        loginCanvas.SetActive(false);
        mainMenu.SetActive(true);

        Button buttonToSelect = mainMenu.GetComponentInChildren<Button>();

        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject); //selection the button you dropped in the inspector
    }

    void GotoAdminMenu()
    {
        passwordInput.gameObject.SetActive(false);
        loginCanvas.SetActive(false);
        adminMenuCanvas.SetActive(true);

        Button buttonToSelect = adminMenuCanvas.GetComponentInChildren<Button>();

        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject); //selection the button you dropped in the inspector
    }

    void GotoSessionDetails()
    {
        passwordInput.gameObject.SetActive(false);
        loginCanvas.SetActive(false);
        sessionDetailsCanvas.SetActive(true);

        List<Session> sessions = sessionControler.FindSessionsByUserName(PlayerPrefs.GetString("username"));
        Session LastSession = sessions[sessions.Count - 1];

        if (LastSession.mapName == "ExamMap")
        {
            string[] strings = LastSession.examDetails();
            sessionDetailsTitle.text = strings[0];
            sessionDetails.text = strings[1];
        }
        else
        {
            sessionDetailsTitle.text = "Drive Summary";
            sessionDetails.text = LastSession.sessionDetails();
        }

        Button buttonToSelect = sessionDetailsCanvas.GetComponentInChildren<Button>();

        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject); //selection the button you dropped in the inspector
    }


}
