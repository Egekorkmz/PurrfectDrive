using System;
using System.Text;
using Logitech;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

public class PlayerCarInputHandler : MonoBehaviour
{
    CarDataBusInput dataPackage;
    LogitechGSDK.LogiControllerPropertiesData properties;
    bool isWheelConnected;
    bool isCarRunning = false;
    const int LOGI_INPUT_RESOLUTION = 32767;
    const int VPP_INPUT_RESOLUTION = 10000;
    PlayerCarController playerCarController;
    
    public GameObject car;

    private void Awake()
    {
        dataPackage = new CarDataBusInput();
        playerCarController = car.GetComponent<PlayerCarController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogitechGSDK.LogiSteeringInitialize(false);

    }
    void OnApplicationQuit()
    {
        LogitechGSDK.LogiSteeringShutdown();
    }

    // Update is called once per frame
    void Update()
    {
        isWheelConnected = LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0);
        // Debug.Log("Device connection: " + isWheelConnected);

        if (isWheelConnected) { 

            //CONTROLLER STATE
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);
            
            dataPackage.Steer = (int)((rec.lX / (double)LOGI_INPUT_RESOLUTION) * VPP_INPUT_RESOLUTION);
            dataPackage.Throttle = VPP_INPUT_RESOLUTION - (int)((VPP_INPUT_RESOLUTION / 2.0) + (rec.rglSlider[0] / (double)LOGI_INPUT_RESOLUTION) * (VPP_INPUT_RESOLUTION / 2.0));
            dataPackage.Brake = VPP_INPUT_RESOLUTION - (int)((VPP_INPUT_RESOLUTION / 2.0) + (rec.lRz / (double)LOGI_INPUT_RESOLUTION) * (VPP_INPUT_RESOLUTION / 2.0));
            dataPackage.Clutch = VPP_INPUT_RESOLUTION - (int)((VPP_INPUT_RESOLUTION / 2.0) + (rec.lY / (double)LOGI_INPUT_RESOLUTION) * (VPP_INPUT_RESOLUTION / 2.0));
            //Debug.Log(dataPackage.Steer);
            //Debug.Log(dataPackage.Throttle);
            //Debug.Log(dataPackage.Brake);
            //Debug.Log(dataPackage.Clutch);

            if (rec.rgbButtons[0] == 128)
            {
                if (dataPackage.Key == -1)
                {
                    dataPackage.Key = 1;
                }
            }

            playerCarController.recieveDataInput(dataPackage);

            //switch (rec.rgdwPOV[0])
            //{
            //    case (0): actualState += "POV : UP\n"; break;
            //    case (4500): actualState += "POV : UP-RIGHT\n"; break;
            //    case (9000): actualState += "POV : RIGHT\n"; break;
            //    case (13500): actualState += "POV : DOWN-RIGHT\n"; break;
            //    case (18000): actualState += "POV : DOWN\n"; break;
            //    case (22500): actualState += "POV : DOWN-LEFT\n"; break;
            //    case (27000): actualState += "POV : LEFT\n"; break;
            //    case (31500): actualState += "POV : UP-LEFT\n"; break;
            //    default: actualState += "POV : CENTER\n"; break;
            //}

            ////Button status :

            //buttonStatus = "Button pressed : \n\n";
            //for (int i = 0; i < 128; i++)
            //{
            //    if (rec.rgbButtons[i] == 128)
            //    {
            //        buttonStatus += "Button " + i + " pressed\n";
            //    }

            //}
            ////shifter status
            //int shifterTipe = LogitechGSDK.LogiGetShifterMode(0);
            //string shifterString = "";
            //if (shifterTipe == 1) shifterString = "Gated";
            //else if (shifterTipe == 0) shifterString = "Sequential";
            //else shifterString = "Unknown";
            //actualState += "\nSHIFTER MODE:" + shifterString;


            //CONTROLLER PROPERTIES
            //LogitechGSDK.LogiControllerPropertiesData actualProperties = new LogitechGSDK.LogiControllerPropertiesData();
            //LogitechGSDK.LogiGetCurrentControllerProperties(0, ref actualProperties);
            // actualProperties.forceEnable
            // actualProperties.overallGain
            // actualProperties.springGain
            // actualProperties.damperGain
            // actualProperties.defaultSpringEnabled
            // actualProperties.combinePedals 
            // actualProperties.wheelRange
            // actualProperties.gameSettingsEnabled
            // actualProperties.allowGameSettings
        }

    }
}
