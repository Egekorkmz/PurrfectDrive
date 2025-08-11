using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ViolationTypes;

[System.Serializable]
public class Session
{
    public string sessionOwner = "admin";
    public string sessionID;
    public string mapName;
    public string sessionDuration;
    public int SessionPoint = 1000;

    //doesn't saved into json
    public DateTime startTime;
    public string startTimeAsString;
    public DateTime endTime;

    //only userErrorsAsString is saved into json
    public string userErrorsAsString;
    public Dictionary<string, UserError> userErrors;

    public Session(string owner)
    {
        sessionOwner = owner;
        sessionID = sessionOwner + DateTime.Now.ToString("yyyyMMddHHmm");
        startTime = DateTime.Now;
        startTimeAsString = startTime.ToString("HH:mm dd/MM/yyyy");
        mapName = PlayerPrefs.GetString("mapName");
        userErrors = new Dictionary<string, UserError>();
    }

    public Session()
    {
        sessionID = sessionOwner + DateTime.Now.ToString("yyyyMMddHHmm");
        startTime = DateTime.Now;
        startTimeAsString = startTime.ToString("HH:mm dd/MM/yyyy");
        mapName = PlayerPrefs.GetString("mapName");
        userErrors = new Dictionary<string, UserError>();
    }

    public bool addError (Violation violation)
    {
        if (userErrors.ContainsKey(violation.Code))
        {
            userErrors[violation.Code].errorCount++;
            SessionPoint -= violation.Value;
        }
        else
        {
            userErrors.Add(violation.Code, new UserError(violation));
            SessionPoint -= violation.Value;
        }
        Debug.Log("Session Point: " + SessionPoint);
        return SessionPoint <= 0;
    }
    public void createUserErrorString()
    {
        userErrorsAsString = "";
        if (userErrors == null || userErrors.Count == 0)
        {
            return; 
        }
        else
        {
            string[] errors = new string[userErrors.Count];
            int i = 0;
            foreach (var error in userErrors)
            {
                errors[i++] = error.Value.toJson();
            }

            userErrorsAsString = "[" + string.Join("/", errors) + "]";
            Debug.Log(userErrorsAsString);
        }
    }

    public string toJson()
    {
        createUserErrorString();
        return JsonUtility.ToJson(this, true);
    }

    public Dictionary<string, UserError> DecodeUserErrors()
    {
        Dictionary<string, UserError> newErrors = new Dictionary<string, UserError>();
        if (string.IsNullOrEmpty(userErrorsAsString))
        {
            return newErrors;
        }

        string[] errors = userErrorsAsString.Trim('[', ']').Split('/');

        foreach (var error in errors)
        {
            UserError userError = JsonUtility.FromJson<UserError>(error);
            newErrors.Add(userError.errorName, userError);
        }

        userErrors = newErrors;
        return newErrors;
    }

    //TODO : make refactor on getting error list
    public string GetErrorList()
    {
        //string erorrList = "Sesion Time: " + sessionDuration + "\n";

        string errorList = "";

        if (userErrors == null || userErrors.Count == 0)
        {
            errorList += "No errors";
        } else         {
            foreach (var error in userErrors)
            {
                errorList += error.Value.toString() + "\n";
            }
        }

        return errorList;
    }

    public string sessionDetails()
    {

        string sessionDetails = "Driver: " + sessionOwner + "\n";
        sessionDetails += "Map: " + mapName + "\n";
        sessionDetails += "Duration: " + sessionDuration + "\n";
        sessionDetails += "Driver Mistakes:\n" + GetErrorList();
        return sessionDetails;
    }

    public string[] examDetails()
    {
        string[] examStrings;

        string examMessage = "";
        if (SessionPoint > 900)
        {
            examMessage = "Amazing! You've Reached Your Goal";
        }
        else if (SessionPoint > 700)
        {
            examMessage = "Doing Well, Push a Little Further";
        }
        else if (SessionPoint > 500)
        {
            examMessage = "You're on the Right Track, Keep Going";
        }
        else if (SessionPoint > 200)
        {
            examMessage = "You Need to Work Much Harder";
        }
        else
        {
            examMessage = "Failed";
        }

        string details = "Driver: " + sessionOwner + "\n";
        details += "Duration: " + sessionDuration + "\n";
        details += "Driver Mistakes:\n" + GetErrorList();
        details += "\nSession Point: " + SessionPoint + " / 1000\n";

        examStrings = new string[] { examMessage, details };
        return examStrings;
    }

}