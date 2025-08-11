using UnityEngine;
using VehiclePhysics;

public class TurnSignals : MonoBehaviour
{
    public GameObject leftSignalLight; //signal mash
    public GameObject rightSignalLight; //signal mash
    public float blinkInterval = 0.5f; // Yanýp sönme süresi
    public AudioSource audioSource;
    private bool m_ToggleChange = false; //to make audio play once
   

    public bool isLeftSignalOn = false;
    public bool isRightSignalOn = false;
    private bool isHazardsOn = false;
    private bool isAudioPlaying = false;
    private float signalTimer = 0;
    private bool preClose = false;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (isLeftSignalOn)
        {
            if (horizontalInput < -0.15)
            {
                preClose = true;
            }
            else if (horizontalInput > 0 && preClose)
            {
                isLeftSignalOn = !isLeftSignalOn;
                m_ToggleChange = true;
                isRightSignalOn = false;
                isHazardsOn = false;
                leftSignalLight.SetActive(false);
                rightSignalLight.SetActive(false);
                signalTimer = 0;
                preClose = false;
            }
        } else if (isRightSignalOn)
        {

            if (horizontalInput > 0.15)
            {
                preClose = true;
            }
            else if (horizontalInput < 0 && preClose)
            {
                isRightSignalOn = !isRightSignalOn;
                m_ToggleChange = true;
                isLeftSignalOn = false;
                isHazardsOn = false;
                leftSignalLight.SetActive(false);
                rightSignalLight.SetActive(false);
                signalTimer = 0;
                preClose = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("joystick button 5")) // left signal
        {
            isLeftSignalOn = !isLeftSignalOn;
            m_ToggleChange = true;
            isRightSignalOn = false;
            isHazardsOn = false;
            leftSignalLight.SetActive(false);
            rightSignalLight.SetActive(false);
            signalTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("joystick button 4")) // right signal
        {
            isRightSignalOn = !isRightSignalOn;
            m_ToggleChange = true;
            isLeftSignalOn = false;
            isHazardsOn = false;
            leftSignalLight.SetActive(false);
            rightSignalLight.SetActive(false);
            signalTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) // right signal
        {
            isHazardsOn = !isHazardsOn;
            m_ToggleChange = true;
            isLeftSignalOn = false;
            isRightSignalOn = false;
            leftSignalLight.SetActive(false);
            rightSignalLight.SetActive(false);
            signalTimer = 0;
        }

        if (isLeftSignalOn)
        {
            isAudioPlaying = true;
            blinkSignal(leftSignalLight);
        }
        else if (isRightSignalOn)
        {
            isAudioPlaying = true;
            blinkSignal(rightSignalLight);
        }
        else if (isHazardsOn)
        {
            isAudioPlaying = true;
            blinkHazards();
        }
        else
        {
            isAudioPlaying = false;
        }

        playAudio();
    }

    private void blinkSignal(GameObject signalLight)
    {
        signalTimer += Time.deltaTime;

        if (Mathf.Floor(signalTimer * 2) % 2 == 0)
        {
            signalLight.SetActive(true);
        }
        else
        {
            signalLight.SetActive(false);
        }
    }

    private void blinkHazards()
    {
        signalTimer += Time.deltaTime;

        if (Mathf.Floor(signalTimer * 2) % 2 == 0)
        {
            leftSignalLight.SetActive(true);
            rightSignalLight.SetActive(true);
        }
        else
        {
            leftSignalLight.SetActive(false);
            rightSignalLight.SetActive(false);
        }
    }

    private void playAudio()
    {
        if (isAudioPlaying == true && m_ToggleChange == true)
        {
            //Play the audio you attach to the AudioSource component
            audioSource.Play();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = false;
        }

        if (isAudioPlaying == false && m_ToggleChange == true)
        {
            //Stop the audio
            audioSource.Stop();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = false;
        }
    }
}

