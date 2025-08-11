using UnityEngine;
using VehiclePhysics;

public class PlayerCarController : VehicleBehaviour
{
    CarDataBusInput dataPackage;

    public void recieveDataInput(CarDataBusInput incoming)
    {
        dataPackage = incoming;
    }
    // Update is called once per frame
    void Update()
    {
        vehicle.data.Set(Channel.Input, InputData.Steer, (int)(Input.GetAxis("Horizontal") * 10000));
    }
}
