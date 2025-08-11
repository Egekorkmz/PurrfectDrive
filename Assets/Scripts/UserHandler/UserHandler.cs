using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserHandler : MonoBehaviour
{
    [SerializeField] private string filePath = "data";
    private List<User> users = new List<User>();

    private void Start()
    {
        filePath = Path.Combine("data", "users.json");
        Debug.Log($"File path: {filePath}");
        users = GetUsers();
    }

    // Save a list of users to the JSON file
    public void SaveUsers()
    {
        string json = JsonUtility.ToJson(new UserListWrapper { Users = users });
        File.WriteAllText(filePath, json);
    }

    // Get the list of users from the JSON file
    public List<User> GetUsers()
    {
        if (!File.Exists(filePath))
        {
            return new List<User>();
        }

        string json = File.ReadAllText(filePath);
        UserListWrapper wrapper = JsonUtility.FromJson<UserListWrapper>(json);
        return wrapper.Users ?? new List<User>();
    }

    // Find and return a user in the list of users
    public User FindUser(string username)
    {
        return users.Find(user => user.username == username);
    }

    // Create a new user and save it to the JSON file
    public bool CreateAndSaveUser(string username)
    {
        if (users.Find(user => user.username == username) == null)
        {
            users.Add(new User(username));
            SaveUsers();

            return true;
        }
        return false;
    }

    [System.Serializable]
    private class UserListWrapper
    {
        public List<User> Users;
    }
}
