using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] menus;
    private Button buttonToSelect;
    public void quitAppplication()
    {
        Debug.Log("Application is exiting");
        Application.Quit();
    }
    
    public void goToMenu(GameObject menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        } 
        
        menu.SetActive(true);

        buttonToSelect = menu.GetComponentInChildren<Button>();
        highlightButton(buttonToSelect.gameObject);

    }
    
    public void goToScene(string sceneName)
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("mapName", sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void RestartSession()
    {
        Time.timeScale = 1f;
        Debug.Log("Session Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void highlightButton(GameObject hightlight)
    {
        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(hightlight); //selection the button you dropped in the inspector
    }

}
