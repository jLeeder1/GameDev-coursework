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
        teamType = TeamType.RED_TEAM;
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        nPCGroundDetection = GetComponent<NPCGroundDetection>();
        //navMeshAgent.SetDestination(targetGameObject.transform.position);
        EntityUniqueIdentifier = $"{GameConstants.NPC_TAG}{GameConstants.NPC_COUNTER}";
        GameConstants.NPC_COUNTER++;
        playerObject = GameObject.FindGameObjectWithTag(GameConstants.PLAYER_TAG);
    }

    public override void SpecialisedSpawnHelper()
    {
        // NPC specific
        nPCGroundDetection.ToggleAllDetectGroundWithRayInChildren(true);
        navMeshAgent.enabled = true;
        navMeshAgent.velocity = new Vector3(0f, 0f, 0f);
    }
}
