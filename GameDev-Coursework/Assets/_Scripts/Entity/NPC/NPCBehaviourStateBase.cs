using UnityEngine;
using UnityEngine.AI;

public abstract class NPCBehaviourStateBase : MonoBehaviour
{
    public Vector3 CurrentDestination;
    protected NavMeshAgent navMeshAgent;
    protected NPC npc;
    protected int navMeshWalkableArea;
    protected bool isRedTeam;

    protected void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshWalkableArea = NavMesh.GetAreaFromName("Walkable");
        //navMeshWalkableArea = 1 << navMeshWalkableArea;
        navMeshWalkableArea = 0;
        npc = GetComponent<NPC>();
        isRedTeam = npc.isRedTeam;
    }

    protected void SetDestination(Vector3 newDestination)
    {
        CurrentDestination = newDestination;
        navMeshAgent.SetDestination(CurrentDestination);
    }

    public abstract void PerformBehaviour();
    public abstract bool ShouldTriggerBehaviour();
}
