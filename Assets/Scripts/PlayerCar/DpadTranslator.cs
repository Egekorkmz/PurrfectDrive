using System.Collections;
using UnityEngine;

public class DpadTranslator : MonoBehaviour
{
    public int up_buttonDown;
    public int down_buttonDown;
    public int left_buttonDown;
    public int right_buttonDown;
    [SerializeField] private bool up_chkDown;
    [SerializeField] private bool down_chkDown;
    [SerializeField] private bool left_chkDown;
    [SerializeField] private bool right_chkDown;

    private void Update()
    {
        if (Input.GetAxis("DpadY") > 0 && !up_chkDown)
        {
            up_chkDown = true;
            StartCoroutine(Up_ButtonDownCo());
        }
        else if (Input.GetAxis("vertical") == 0)
        {
            up_chkDown = false;
        }

        if (Input.GetAxis("DpadY") < 0 && !down_chkDown)
        {
            up_chkDown = true;
            StartCoroutine(Up_ButtonDownCo());
        }
        else if (Input.GetAxis("vertical") == 0)
        {
            up_chkDown = false;
        }
    }


    IEnumerator Up_ButtonDownCo()
    {
        up_buttonDown = 1;
        yield return new WaitForEndOfFrame();
        up_buttonDown = 0;
    }
}
