using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrolBehaviour : NPCBehaviourStateBase
{
    private List<Vector3> patrolPoints;
    private float samplePositionRadius;

    protected void Awake()
    {
        base.Awake();
        patrolPoints = new List<Vector3>();
        samplePositionRadius = 200f;
    }

    public override void PerformBehaviour()
    {
        if(patrolPoints.Count == 0)
        {
            FindRandomPointOnNavMesh();
        }

        CurrentDestination = patrolPoints.First();
        float distanceToNextPatrolPoint = Vector3.Distance(transform.position, CurrentDestination);
        SetDestination(CurrentDestination);

        if(distanceToNextPatrolPoint < 2f)
        {
            patrolPoints.Remove(CurrentDestination);

            if(patrolPoints.Count > 0)
            {
                CurrentDestination = patrolPoints.First();
            }
        }
    }

    private void FindRandomPointOnNavMesh()
    {
        int attempts = 0;
        Vector3 randomDirection = Random.insideUnitSphere * samplePositionRadius;
        randomDirection += transform.position;

        while (patrolPoints.Count < 2)
        {
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomDirection, out hit, samplePositionRadius, 1 << NavMesh.GetAreaFromName("Walkable")))
            {
                patrolPoints.Add(hit.position);
            }

            attempts++;
            if (attempts >= 100)
            {
                patrolPoints.Add(npc.goalToScoreIn.transform.position);
                //navMeshAgent.SetDestination(npc.goalToScoreIn.transform.position);
                break;
            }
        }
    }

    // Always true as this will be the default behaviour if no others can be chosen
    public override bool ShouldTriggerBehaviour() => true;
}
