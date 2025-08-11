using UnityEngine;
using static ViolationTypes;

public class UserError
{
    public int errorCount = 1;
    public string errorName;
    public int errorPoint;

    public UserError(Violation violation)
    {
        errorName = violation.Code;
        errorPoint = violation.Value;
    }

    public string toJson()
    {
        return JsonUtility.ToJson(this);
    }

    public string toString() => Violation.GetViolationDescription(errorName) + ": " + errorCount;

}
