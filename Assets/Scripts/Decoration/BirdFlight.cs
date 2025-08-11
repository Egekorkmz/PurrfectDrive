using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class BirdFlight : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed          = 6f;
    public float turnSpeed      = 4f;
    public float waypointRadius = 0.5f;
    private int _index;

    void Update()
    {
        if (waypoints.Count == 0) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            waypoints[_index].position,
            speed * Time.deltaTime
        );

        var dir = waypoints[_index].position - transform.position;
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                turnSpeed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, waypoints[_index].position) < waypointRadius)
            _index = (_index + 1) % waypoints.Count;
    }
}
