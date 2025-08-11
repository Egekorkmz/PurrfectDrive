using UnityEngine;

public class ExamEventHandler : MonoBehaviour
{
    public ExamController examController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crashDetector.examFailed += examFailed;
        crashDetector.examFinished += examFinished;
        ViolationEventHandler.examFailed += examFailed;

    }

    private void OnDestroy()
    {
        crashDetector.examFailed -= examFailed;
        crashDetector.examFinished -= examFinished;
        ViolationEventHandler.examFailed -= examFailed;
    }

    void examFailed()
    {
        Debug.Log("Exam failed");
        examController.HandleExamFailure();
    }

    void examFinished()
    {
        Debug.Log("Exam finished");
        examController.HandleExamFinish();
    }
}
