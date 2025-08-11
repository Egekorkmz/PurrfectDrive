using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{

    public GameObject startMenu;
    public GameObject[] lights;
    public AudioClip startSound;
    public AudioSource startAudioSource;
    public PauseMenu pauseMenu;

    void Start()
    {
        StartCoroutine(PlayStart());
    }

    IEnumerator PlayStart()
    {
        pauseMenu.pausable = false;
        //to make sure they are invisible
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
        startMenu.SetActive(false);

        startMenu.SetActive(true);
        yield return new WaitForSeconds(2f);
        startAudioSource.PlayOneShot(startSound, 0.5f);
        yield return new WaitForSeconds(0.3f);
        lights[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        lights[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        lights[2].SetActive(true);
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
        startMenu.SetActive(false);
        pauseMenu.pausable = true;

        yield return null;
    }



}
