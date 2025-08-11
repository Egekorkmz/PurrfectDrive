using System.Text.RegularExpressions;
using System;
using UnityEngine;
using UnityEngine.Windows;
using static ViolationTypes;

public class TurnSignalDetector : MonoBehaviour
{
    public static event Action<Violation> OnWarningMessagePopup;

    private int turn_in_number = 0;
    private int turn_out_number = 0;
    public TurnSignals signals;
    private enum Direction {
        left = 1,
        right = 2,
        straight = 0
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("turn_in"))
        {
            Debug.Log("Trigger Entered: " + other.gameObject.name);

            turn_in_number = getIntFromName(other.gameObject.name);   
        } 
        else if (other.gameObject.CompareTag("turn_out"))
        {
            turn_out_number = getIntFromName(other.gameObject.name);
            switch (turn_in_number)
            {
                case 1:
                    switch (turn_out_number)
                    {
                        case 2:
                            isTurnValid(Direction.right);
                            break;
                        case 3:
                            isTurnValid(Direction.straight);
                            break;
                        case 4:
                            isTurnValid(Direction.left);
                            break;
                    }
                    break;
                case 2:
                    switch (turn_out_number)
                    {
                        case 1:
                            isTurnValid(Direction.left);
                            break;
                        case 3:
                            isTurnValid(Direction.right);
                            break;
                        case 4:
                            isTurnValid(Direction.straight);
                            break;
                    }
                    break;
                case 3:
                    switch (turn_out_number)
                    {
                        case 1:
                            isTurnValid(Direction.straight);
                            break;
                        case 2:
                            isTurnValid(Direction.left);
                            break;
                        case 4:
                            isTurnValid(Direction.right);
                            break;
                    }
                    break;
                case 4:
                    switch (turn_out_number)
                    {
                        case 1:
                            isTurnValid(Direction.right);
                            break;
                        case 2:
                            isTurnValid(Direction.straight);
                            break;
                        case 3:
                            isTurnValid(Direction.left);
                            break;
                    }
                    break;
            }
            turn_in_number = 0;
        }
    }

    private int getIntFromName(string str)
    {
        Match match = Regex.Match(str, @"\d+");

        if (match.Success && int.TryParse(match.Value, out int number))
        {
            return number;
        }
        return 0;
    }

    private void isTurnValid (Direction turnType)
    {
        switch (turnType)
        {

            case Direction.right:
                if (signals.isRightSignalOn == false)
                {
                    //🚘 You turned without using your signal!
                    OnWarningMessagePopup?.Invoke(Violation.GetViolation(ViolationCode.signal));
                }
                break;
            case Direction.left:
                if (signals.isLeftSignalOn == false)
                {
                    OnWarningMessagePopup?.Invoke(Violation.GetViolation(ViolationCode.signal));
                }
                break;
        }
    }
}
