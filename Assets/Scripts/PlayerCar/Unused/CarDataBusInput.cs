using UnityEngine;

public sealed class CarDataBusInput
{
    // fields are according to Vehiicle Pro's Data Bus Input Channels
    // https://vehiclephysics.com/advanced/databus-reference/

    private int steer; // -10000 <-> 10000
    private int brake; // 0 <-> 10000
    private int throttle; // 0 <-> 10000
    private int handbrake; // 0 <-> 10000
    private int clutch; // 0 <-> 10000
    private int manual_gear; // -1, 0 ,1 ,2...
    private int automatic_gear; // 0, 1, 2, 3, 4...
    private int gear_shift; // -1, 1
    private int retarder; //0, 1, 2, 3... (unused)
    private int key; // -1, 0, 1

    public CarDataBusInput()
    {
        steer = 0;
        brake = 0;
        throttle = 0;
        handbrake = 0;
        clutch = 0;
        manual_gear = 0;
        automatic_gear = 0;
        gear_shift = 0;
        retarder = 0;
        key = -1;
    }

    public int Steer { get => steer; set => steer = value; }
    public int Throttle { get => throttle; set => throttle = value; }
    public int Brake { get => brake; set => brake = value; }
    public int Handbrake { get => handbrake; set => handbrake = value; }
    public int Clutch { get => clutch; set => clutch = value; }
    public int Manual_gear { get => manual_gear; set => manual_gear = value; }
    public int Automatic_gear { get => automatic_gear; set => automatic_gear = value; }
    public int Gear_shift { get => gear_shift; set => gear_shift = value; }
    public int Retarder { get => retarder; set => retarder = value; }
    public int Key { get => key; set => key = value; }

    public override string ToString()
    {
        return "Steer: " + Steer + "\nThrottle: " + Throttle + "\nBrake: " + Brake + "\nHandbrake: " + Handbrake + "\nClutch: " + Clutch
            + "\nManual Gear: " + Manual_gear + "\nAutomatic Gear: " + Automatic_gear + "\nGear Shift: " + Gear_shift + "\nRetarder: " + Retarder + "\nKey: " + Key;
    }
}
