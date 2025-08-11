using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrainingMapCarController : MonoBehaviour
{
    public GameObject congratsUserMenu;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered: " + other.gameObject.name);
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Finish Line Reached");
            congratsUser();
        }
    }

    public void congratsUser()
    {
        congratsUserMenu.SetActive(true);
        Button buttonToSelect = congratsUserMenu.GetComponentInChildren<Button>();
        highlightButton(buttonToSelect.gameObject);

        Time.timeScale = 0f;
        enabled = true;
    }

    public void setComingFrom()
    {
        PlayerPrefs.SetString("comingFrom", "training");
    }
    private void highlightButton(GameObject hightlight)
    {
        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(hightlight); //selection the button you dropped in the inspector
    }
}
