using System.Security.Cryptography;
using System.Text;
using UnityEngine;

[System.Serializable]
public class User
{
    public string username;
    //public string passwordHash;

    public User(string username)
    {
        this.username = username;
        //this.passwordHash = ComputeSHA256Hash(password);
    }

    /*
    public bool CheckUserPassword(string password)
    {
        string passwordHashToCheck = ComputeSHA256Hash(password);
        return this.passwordHash == passwordHashToCheck;
    }

    private string ComputeSHA256Hash(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
    */
}
