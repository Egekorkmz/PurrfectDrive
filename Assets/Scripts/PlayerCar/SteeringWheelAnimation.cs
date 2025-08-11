using UnityEngine;
using UnityEngine.InputSystem;

public class SteeringWheelAnimation : MonoBehaviour
{
    private PlayerInput m_PlayerInput;
    private InputAction m_SteerAction;
    private Vector2 steeringInput;

    float horizontalInput;
    [SerializeField] public Transform SteeringWheelTransform;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalInput);
        SteeringWheelTransform.localRotation = Quaternion.Euler(0, 0, horizontalInput * -450);
    }

}
