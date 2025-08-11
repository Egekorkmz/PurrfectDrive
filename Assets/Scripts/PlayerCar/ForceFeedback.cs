using Logitech;
using UnityEngine;
using VehiclePhysics;

public class ForceFeedback : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;
    public VPVehicleToolkit vehicleTool;
    public VehicleBase vehicle;
    int forceFeedBack;
    float slipAngle;
    float verticalLoadOnTire;
    float Dlat = 0.01f;
    float Clat = 2.1f;
    float Blat = -0.3f;
    float Elat = -10f;

    float inverseMultiplier = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogitechGSDK.LogiSteeringInitialize(false);
    }

    // Update is called once per frame
    void Update()
    { 
        //Debug.Log(vehicleTool.lateralG * 100);
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            //slipAngle = 0;
            //verticalLoadOnTire = 0;
            //for (int i = 0; i < 2; i++)
            //{
            //    VehicleBase.WheelState ws = vehicle.wheelState[i];

            //    //Debug.Log(ws.combinedTireSlip + ws.verticalForce);

            //    Debug.Log(Mathf.Atan2(ws.tireSlip.y, ws.tireSlip.x) + 1);

            //    slipAngle += ws.combinedTireSlip;
            //    verticalLoadOnTire += ws.verticalForce;

            //}
            //slipAngle /= 2;
            //verticalLoadOnTire /= 2;

            //forceFeedBack = (int)((Dlat * Mathf.Sin(Clat * Mathf.Atan((Blat * slipAngle) - (Elat * ((Blat * slipAngle) - (Mathf.Atan(Blat * slipAngle))))))) * verticalLoadOnTire);


            //LogitechGSDK.LogiPlayConstantForce(0, forceFeedBack);
            LogitechGSDK.LogiPlaySpringForce(0, 0, 15, 10);
        }
    }
}
