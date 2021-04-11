using UnityEngine;
using UnityEngine.AI;

public class NPC : Entity
{
    public GameObject targetGameObject;

    public NPCGroundDetection nPCGroundDetection { get; private set; }

    public override string entityPrefabType { get => "NPC"; }

    private void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetGameObject.transform.position);
        EntityUniqueIdentifier = $"{GameConstants.NPC_TAG}{GameConstants.NPC_COUNTER}";
        GameConstants.NPC_COUNTER++;
    }
}
