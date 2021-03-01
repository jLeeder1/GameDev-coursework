using UnityEngine;
using UnityEngine.AI;

public class BoundaryNavMeshAgentDisable : MonoBehaviour
{
    [SerializeField]
    private LayerMask NPCLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstants.NPC_TAG))
        {
            other.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}
