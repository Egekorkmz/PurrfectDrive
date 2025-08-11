// NPCWalker.cs
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCWalker : MonoBehaviour
{
    [Tooltip("First waypoint the NPC should start at")]
    public Waypoint firstWaypoint;

    [SerializeField] float arriveThreshold = .3f;

    NavMeshAgent agent;
    Animator     animator;
    Waypoint     current;

    void Start()
    {
        agent    = GetComponent<NavMeshAgent>();
        animator = TryGetComponent(out Animator a) ? a : null;

        if (firstWaypoint == null)
        {
            enabled = false;
            return;
        }

        current = firstWaypoint;

        // Warp onto the NavMesh at the first waypoint
        Vector3 spawnPos = current.GetPosition();
        if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            spawnPos = hit.position;
        agent.Warp(spawnPos);

        SetDestination(current);
        if (animator)
            animator.SetBool("isWalking", true);
    }

    void Update()
    {
        if (agent.pathPending || !agent.isOnNavMesh)
            return;

        if (agent.remainingDistance <= arriveThreshold)
            Advance();

        if (animator)
            animator.SetBool("isWalking", agent.velocity.sqrMagnitude > 0.01f);
    }

    void Advance()
    {
        // Always prefer the alternate branch if available, otherwise go to next
        Waypoint chosen = (current.alternateNextWaypoint != null
                           && Random.value < current.branchChance)
                          ? current.alternateNextWaypoint
                          : current.nextWaypoint;

        // If we've reached the end of the chain, loop back to the first waypoint
        current = chosen != null ? chosen : firstWaypoint;

        SetDestination(current);
    }

    void SetDestination(Waypoint wp)
    {
        Vector3 target = wp.GetPosition();
        if (NavMesh.SamplePosition(target, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            target = hit.position;
        agent.SetDestination(target);
    }
}
