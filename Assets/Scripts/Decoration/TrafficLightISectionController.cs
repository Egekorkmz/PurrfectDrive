using System.Collections;
using UnityEngine;

public class TrafficLightISectionController : MonoBehaviour
{
    public TrafficLightController[] TrafficLights;

    void Start()
    {
        StartCoroutine(TrafficLightSectionCycle());
    }

    private IEnumerator TrafficLightSectionCycle()
    {
        while (true) {
        for (int i = 0; i < TrafficLights.Length; i++)
            {
                yield return TrafficLights[i].TrafficLightCycle();
            }
        }
    }
}
