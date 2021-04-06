using UnityEngine;
using UnityEngine.AI;

public class NPC : Entity
{
    [SerializeField]
    GameObject targetGameObject;

    public NPCGroundDetection nPCGroundDetection { get; private set; }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetGameObject.transform.position);
    }
}
