using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateUserMenu : MonoBehaviour
{
    public TMP_InputField usernameInput;
    //public TMP_InputField passwordInput;
    public TextMeshProUGUI feedbackText;

    public UserHandler userHandler;

    public GameObject createUserCanvas;
    public GameObject mainMenu;
   
    public void CreateUser()
    {
        string enteredUsername = usernameInput.text;
        //string enteredPassword = passwordInput.text;


        bool response = userHandler.CreateAndSaveUser(enteredUsername);

        // add user logic to here
        if (response)
        {
            feedbackText.text = "User created sucessfully.";
            feedbackText.color = Color.green;

            
            Invoke("GotoMainMenu", 1.5f);
        }
        else
        {
            feedbackText.text = "User already exist.";
            feedbackText.color = Color.red;
        }
    }

    void GotoMainMenu()
    {
        createUserCanvas.SetActive(false);
        mainMenu.SetActive(true);

        Button buttonToSelect = mainMenu.GetComponentInChildren<Button>();

        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(buttonToSelect.gameObject); //selection the button you dropped in the inspector
    }
}
