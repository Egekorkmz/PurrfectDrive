// Waypoint.cs
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Waypoint : MonoBehaviour
{
    [Tooltip("Linear chain links")]
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;

    [Tooltip("If set, the walker can branch here to this waypoint")]
    public Waypoint alternateNextWaypoint;

    [Range(0f, 1f), Tooltip("Chance [0–1] to take the alternate branch")]
    public float branchChance = 0.5f;

    [Range(0f, 5f)]
    public float width = 1f;

    public Vector3 GetPosition()
    {
        float half = width * 0.5f;
        return transform.position + transform.right * Random.Range(-half, half);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 lift      = transform.up * 0.02f;
        Vector3 halfRight = transform.right * 0.5f * width;

        // main sphere
        Gizmos.color = Selection.activeGameObject == gameObject
                       ? Color.yellow
                       : Color.yellow * 0.5f;
        Gizmos.DrawSphere(transform.position + lift, 0.1f);

        // width line
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position - halfRight + lift,
                        transform.position + halfRight + lift);

        // red ↔ previous
        if (previousWaypoint)
        {
            Gizmos.color  = Color.red;
            Vector3 fromR = transform.right * 0.5f * width;
            Vector3 toR   = previousWaypoint.transform.right * 0.5f * previousWaypoint.width;
            Gizmos.DrawLine(transform.position + fromR + lift,
                            previousWaypoint.transform.position + toR + lift);
        }

        // green ↔ next
        if (nextWaypoint)
        {
            Gizmos.color = Color.green;
            Vector3 fromL = transform.right * -0.5f * width;
            Vector3 toL   = nextWaypoint.transform.right * -0.5f * nextWaypoint.width;
            Gizmos.DrawLine(transform.position + fromL + lift,
                            nextWaypoint.transform.position + toL + lift);
        }

        // magenta ↔ alternate branch
        if (alternateNextWaypoint)
        {
            Gizmos.color = Color.magenta;
            Vector3 from = transform.position + lift;
            Vector3 to   = alternateNextWaypoint.transform.position + lift;
            Gizmos.DrawLine(from, to);

            // arrowhead
            Vector3 dir = (from - to).normalized * 0.2f;
            Gizmos.DrawRay(to, Quaternion.Euler(0, 140, 0) * dir);
            Gizmos.DrawRay(to, Quaternion.Euler(0, -140, 0) * dir);
        }
    }
#endif
}
