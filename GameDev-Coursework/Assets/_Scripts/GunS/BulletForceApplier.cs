using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BulletForceApplier : MonoBehaviour
{
    private float npcForce = 25f;
    private float playerForce = 60f;

    private void OnCollisionEnter(Collision collision)
    {
        Entity entity = collision.gameObject.GetComponentInChildren<Entity>();

        if (entity == null)
            return;

        Vector3 directionOfForce = (collision.transform.position - transform.position).normalized;
        directionOfForce.y = 0f;

        NavMeshAgent navMeshAgent = entity.GetComponent<NavMeshAgent>();

        // Handles hitting NPCs
        if (navMeshAgent != null)
        {
            HandleNpcHit(navMeshAgent, directionOfForce, entity, collision);
            return;
        }

        // Handles hitting the player
        HandlePlayerHit(entity, directionOfForce);
    }

    private void HandleNpcHit(NavMeshAgent navMeshAgent, Vector3 directionOfForce, Entity entity, Collision collision)
    {
        if (!navMeshAgent.enabled)
        {
            Rigidbody rigidbody = entity.GetComponent<Rigidbody>();
            ContactPoint positionOfForce = collision.GetContact(0);
            rigidbody.AddForceAtPosition(directionOfForce * npcForce /3, positionOfForce.point, ForceMode.Impulse);
        }
        else
        {
            Vector3 forceVector = directionOfForce * npcForce;
            navMeshAgent.velocity += forceVector;
            entity.GetComponent<NPCGroundDetection>().HandleNavMeshDisable();
            StartCoroutine(DestroyGameObject());
        }
    }

    private void HandlePlayerHit(Entity entity, Vector3 directionOfForce)
    {
        Vector3 playerForceVector = directionOfForce * playerForce;
        CharacterController characterController = entity.GetComponentInChildren<CharacterController>();
        characterController.SimpleMove(playerForceVector);
        StartCoroutine(DestroyGameObject());
    }


    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
