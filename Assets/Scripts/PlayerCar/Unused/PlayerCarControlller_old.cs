using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarControlller : MonoBehaviour
{
    // input manager related
    private PlayerInput m_PlayerInput;
    private InputAction m_GasAction;
    private InputAction m_BrakeAction;
    private InputAction m_SteerAction;
    private InputAction m_EngageGearAction;
    private InputAction m_GearUpAction;
    private InputAction m_GearDownAction;

    // car input related
    private Vector2 steeringInput;
    private float gasInput;
    private float brakeInput;

    // car forces relates

    private float currentMotorRPM;
    private float currentWheelRPM;
    private float currentMotorTorque;

    [SerializeField] private float motorIdleRPM;
    [SerializeField] private float motorTopRPM;
    [SerializeField] AnimationCurve hpToRPMCurve;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;

    // gear related
    private bool isGearEngaged;
    private byte selectedGear;
    [SerializeField] float[] gearRatios;
    [SerializeField] byte firstGear;
    [SerializeField] float differentialRatio;

    // steering related
    private float currentSteerAngle;
    [SerializeField] private float maxSteeringAngle;

    // wheels
    [SerializeField] private WheelCollider FLWheelCollider;
    [SerializeField] private WheelCollider FRWheelCollider;
    [SerializeField] private WheelCollider RLWheelCollider;
    [SerializeField] private WheelCollider RRWheelCollider;

    [SerializeField] private Transform FLWheelTransform;
    [SerializeField] private Transform FRWheelTransform;
    [SerializeField] private Transform RLWheelTransform;
    [SerializeField] private Transform RRWheelTransform;

    private void Start()
    {
        currentMotorRPM = motorIdleRPM;
        isGearEngaged = false;
    }
    private void Update()
    {
        //initiating the input types
        if (m_PlayerInput == null)
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_GasAction = m_PlayerInput.actions["gas"];
            m_BrakeAction = m_PlayerInput.actions["brake"];
            m_SteerAction = m_PlayerInput.actions["steer"];
            m_EngageGearAction = m_PlayerInput.actions["engage gear"];
            m_GearUpAction = m_PlayerInput.actions["gear up"];
            m_GearDownAction = m_PlayerInput.actions["gear down"];
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleGear();
        HandleBreaking();
        HandleSteering();
        UpdateWheels();
    }
    private void HandleMotor()
    {
        Debug.Log(currentMotorRPM);
        Debug.Log(currentMotorTorque);
        Debug.Log(currentWheelRPM);
        //Debug.Log(Mathf.Abs((RLWheelCollider.rpm + RRWheelCollider.rpm) / 2f) * gearRatios[selectedGear] * differentialRatio);
        //Debug.Log(selectedGear);

        calculateMotorPower();
        FLWheelCollider.motorTorque = currentMotorTorque *= gasInput;
        FRWheelCollider.motorTorque = currentMotorTorque *= gasInput;
    }

    //calculates the current motor torque
    private void calculateMotorTorque()
    {
        currentMotorTorque = (hpToRPMCurve.Evaluate(currentMotorRPM / motorTopRPM) * motorForce / currentMotorRPM) * 5252f * gearRatios[selectedGear] * differentialRatio;
    }

    // calculates motor RPM and torque
    private void calculateMotorPower()
    {
        currentWheelRPM = Mathf.Abs((FLWheelCollider.rpm + FRWheelCollider.rpm) / 2f) * gearRatios[selectedGear] * differentialRatio;
        if (!isGearEngaged)
        {
            currentMotorRPM = Mathf.Lerp(currentMotorRPM, Mathf.Max(motorIdleRPM, motorTopRPM * gasInput) + Random.Range(-50, 50), Time.deltaTime);
            currentMotorTorque = 0;
        }
        //TODO: fix later, the code is not good enough for this to work properly 
        //else if (gasInput == 0)
        //{
        //    currentMotorRPM = Mathf.Lerp(currentMotorRPM, Mathf.Max(motorIdleRPM - 100, currentWheelRPM), Time.deltaTime * 3f);
        //    calculateMotorTorque();
        //}
        else
        {
            // TODO: fix this code, the torque causes constant acceleration even if the engine RPM is stable
            currentMotorRPM = Mathf.Lerp(currentWheelRPM / gearRatios[selectedGear], Mathf.Max(motorIdleRPM, motorTopRPM * gasInput) + Random.Range(-50, 50), Time.deltaTime);
            calculateMotorTorque();
        }
    }

    private void HandleGear()
    {
        m_EngageGearAction.performed += context => Debug.Log($"{context.action} performed");
        m_GearUpAction.performed += context => Debug.Log($"{context.action} performed");
        m_GearDownAction.performed += context => Debug.Log($"{context.action} performed");

        // engage/disengage gear
        m_EngageGearAction.performed += context =>
        {
            isGearEngaged = !isGearEngaged;
            if (isGearEngaged)
            {
                // resets the gear position
                selectedGear = firstGear;
            }
        };

        m_GearUpAction.performed += context => 
        {
            if (isGearEngaged && selectedGear < gearRatios.Length - 1)
                selectedGear += 1;
        };
        m_GearDownAction.performed += context =>
        {
            if (isGearEngaged && selectedGear > 0)
                selectedGear -= 1;
        };
    }

    private void HandleBreaking()
    {
        ApplyBrakes();
    }

    private void ApplyBrakes()
    {
        FLWheelCollider.brakeTorque = brakeInput * brakeForce;
        FRWheelCollider.brakeTorque = brakeInput * brakeForce;
        RLWheelCollider.brakeTorque = brakeInput * brakeForce * 0.2f;
        RRWheelCollider.brakeTorque = brakeInput * brakeForce * 0.2f;
    }

    private void GetInput()
    {
        steeringInput = m_SteerAction.ReadValue<Vector2>();
        gasInput = m_GasAction.ReadValue<float>();
        brakeInput = m_BrakeAction.ReadValue<float>();
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * steeringInput.x;
        FLWheelCollider.steerAngle = currentSteerAngle;
        FRWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FLWheelCollider, FLWheelTransform);
        UpdateSingleWheel(FRWheelCollider, FRWheelTransform);
        UpdateSingleWheel(RLWheelCollider, RLWheelTransform);
        UpdateSingleWheel(RRWheelCollider, RRWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
