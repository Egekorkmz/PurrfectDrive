using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public SessionControler sessionControler;
    public bool pausable = true;

    private bool enabled = false;
    void Update()
    {
        if (Input.GetKeyUp("escape") || Input.GetKeyUp("joystick button 8"))
        {
            if (pausable)
            {
                if (enabled)
                {
                    continueGame();
                }
                else
                {
                    pauseGame();
                }
            }
        }
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        enabled = true;

        Button buttonToSelect = pauseMenu.GetComponentInChildren<Button>();
        highlightButton(buttonToSelect.gameObject);
    }
    
    public void continueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        enabled = false;
    }

    private void highlightButton(GameObject hightlight)
    {
        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(hightlight); //selection the button you dropped in the inspector
    }
}
