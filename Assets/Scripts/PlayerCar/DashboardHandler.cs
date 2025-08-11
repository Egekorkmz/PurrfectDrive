using TMPro;
using UnityEngine;
using VehiclePhysics;

public class DashboardHandler : MonoBehaviour
{
    public VPVehicleToolkit vehicle;
    public VehicleBase vehicleData;
    public TextMeshPro modeText;
    public TextMeshPro speedText;
    public GameObject abs;
    public GameObject tractionControl;

    private void Start()
    {
        abs.SetActive(false);
        tractionControl.SetActive(false);
    }
    void Update()
    {
        speedText.text = Mathf.Abs(Mathf.Floor(vehicle.speedInKph)).ToString();
        modeText.text = vehicle.automaticMode.ToString();
        if(vehicleData.data.Get(Channel.Vehicle, VehicleData.AbsEngaged) == 1)
        {
            abs.SetActive(true);
        }
        else
        {
            abs.SetActive(false);
        }
        if (vehicleData.data.Get(Channel.Vehicle, VehicleData.TcsEngaged) == 1 || vehicleData.data.Get(Channel.Vehicle, VehicleData.EscEngaged) == 1)
        {
            tractionControl.SetActive(true);
        }
        else
        {
            tractionControl.SetActive(false);
        }
    }
}
