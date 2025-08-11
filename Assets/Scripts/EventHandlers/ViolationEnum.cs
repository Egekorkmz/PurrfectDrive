using UnityEngine;

public class ViolationTypes : MonoBehaviour
{
    public enum ViolationCode
    {
        crashEnvironment = 0,
        crashNpcHuman = 1,
        signal = 2,
        wrongDirection = 3,
        crossLane = 4,
        exceededSpeed = 5,
        crossRedLight = 6,
    }

    public struct Violation
    {
        public string Code;
        public string Message;
        public int Value;

        // Constructor
        public Violation(string code, string message, int value)
        {
            Code = code;
            Message = message;
            Value = value;
        }

        // Static method to retrieve the message based on ViolationCode
        public static Violation GetViolation(ViolationCode code)
        {
            switch (code)
            {
                case ViolationCode.crossRedLight:
                    return new Violation("crossRedLight", "You ran a red light! Stop when it's red.", 200);
                case ViolationCode.crashEnvironment:
                    return new Violation("crashEnvironment", "You scratched the vehicle! Drive more carefully.", 200);
                case ViolationCode.crashNpcHuman:
                    return new Violation("crashNpcHuman", "You hit a pedestrian! This is a critical violation.", 1000);
                case ViolationCode.signal:
                    return new Violation("signal", "You turned without using your signal!", 40);
                case ViolationCode.wrongDirection:
                    return new Violation("wrongDirection", "You are going in the wrong direction! Please follow the road markings.", 80);
                case ViolationCode.crossLane:
                    return new Violation("crossLane", "You crossed the lane markings repeatedly. Stay within your lane.", 10);
                case ViolationCode.exceededSpeed:
                    return new Violation("exceededSpeed", "You exceeded the speed limit! Slow down.", 10);
                default:
                    return new Violation("Unknown", "No message available.", -1);
            }
        }

        public static string GetViolationDescription(string code)
        {
            switch (code)
            {
                case "crossRedLight":
                    return "Violated red light";
                case "crashEnvironment":
                    return "Crashed";
                case "crashNpcHuman":
                    return "Hit a pedestrian";
                case "signal":
                    return "Failed to use turn signal";
                case "wrongDirection":
                    return "Drove in wrong direction";
                case "crossLane":
                    return "Crossed lane markings";
                case "exceededSpeed":
                    return "Exceeded speed limit";
                default:
                    return "Unknown violation";
            }
        }
    }
}
