using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;

public class SessionControler : MonoBehaviour
{
    [SerializeField] private string saveLocation = "data"; 
    public Session session;

    private void Update()
    {
        //save session when "L" key is pressed debug purposses
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveSession();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            FindSessionsByUserName(PlayerPrefs.GetString("username"));
        }
    }

    public void InitializeSession()
    {
        string username = PlayerPrefs.GetString("username");

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username not found. Cannot initialize session.");
            return;
        }
        session = new Session(username);
    }

    public void SaveSession()
    {
        if (session == null)
        {
            Debug.LogError("Session is not initialized. Cannot save.");
            return;
        }

        try
        {
            //save session end time
            session.endTime = DateTime.Now;
            
            //calculate the duration of the session
            TimeSpan duration = session.endTime - session.startTime;
            session.sessionDuration = duration.ToString(@"hh\:mm\:ss");

            //save session with user errors
            string json = session.toJson();
            string filePath = Path.Combine("data", $"{session.sessionID}.json");
            File.WriteAllText(filePath, json);

            //saving session in PlayerPrefs
            PlayerPrefs.SetString("comingFrom", "session");
            Debug.Log($"Session saved successfully at {filePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save session: {ex.Message}");
        }
    }

    public List<Session> FindSessionsByUserName(string userName)
    {
        List<Session> sessions = new List<Session>();
        try
        {
            if (!Directory.Exists("data"))
            {
                Debug.LogError($"Save location does not exist: {"data"}");
                return sessions;
            }

            string[] files = Directory.GetFiles("data", "*.json");
            foreach (string file in files)
            {
                string json = File.ReadAllText(file);
                Session loadedSession = JsonUtility.FromJson<Session>(json);

                if (loadedSession != null && loadedSession.sessionOwner == userName)
                {
                    loadedSession.DecodeUserErrors();

                    sessions.Add(loadedSession);
                }
            }

            Debug.Log($"Found {sessions.Count} sessions for user: {userName}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to find sessions: {ex.Message}");
        }

        return sessions;
    }
}
