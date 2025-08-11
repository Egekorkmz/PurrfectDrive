using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExamController : MonoBehaviour
{
    public GameObject examFailUI; // Reference to the exam fail UI
    public GameObject examCar; // Reference to the exam car
    public SessionControler sessionControler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetString("comingFrom", "exam");
        StartSession();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartSession()
    {
        Debug.Log("Session Started");
        Time.timeScale = 1;
        if (examFailUI != null)
        {
            examFailUI.SetActive(false); // Ensure the fail UI is hidden at the start
            Button buttonToSelect = examFailUI.GetComponentInChildren<Button>();
            highlightButton(buttonToSelect.gameObject);
        }
    }

    public void HandleExamFailure()
    {
        Debug.Log("Exam Failed");
        Time.timeScale = 0; // Pause the game
        if (examFailUI != null)
        {
            examFailUI.SetActive(true); // Show the fail UI
            Button buttonToSelect = examFailUI.GetComponentInChildren<Button>();
            highlightButton(buttonToSelect.gameObject);
        }
    }

    public void RestartSession()
    {
        Debug.Log("Session Restarted");
        if (examCar != null)
        {
            Debug.Log("Resetting car position and rotation");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void HandleExamFinish()
    {
        Debug.Log("Exam Finished");

        // Oturum bilgilerini kaydet
        PlayerPrefs.SetString("comingFrom", "examFinish");
        sessionControler.SaveSession();

        // Ana menüye yönlendir
        SceneManager.LoadScene("MainMenu");
    }

    private void highlightButton(GameObject hightlight)
    {
        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(hightlight); //selection the button you dropped in the inspector
    }
}
