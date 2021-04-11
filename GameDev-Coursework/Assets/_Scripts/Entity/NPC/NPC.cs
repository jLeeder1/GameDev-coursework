using UnityEngine;
using UnityEngine.AI;

public class NPC : Entity
{
    public GameObject targetGameObject;
    public NPCGroundDetection nPCGroundDetection { get; private set; }
    public GameObject playerObject { get; private set; }


    public override string entityPrefabType { get => "NPC"; }

    protected void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //navMeshAgent.SetDestination(targetGameObject.transform.position);
        EntityUniqueIdentifier = $"{GameConstants.NPC_TAG}{GameConstants.NPC_COUNTER}";
        GameConstants.NPC_COUNTER++;
        playerObject = GameObject.FindGameObjectWithTag(GameConstants.PLAYER_TAG);
    }
}
