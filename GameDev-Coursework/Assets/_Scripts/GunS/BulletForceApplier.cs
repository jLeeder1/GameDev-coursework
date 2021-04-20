using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BulletForceApplier : MonoBehaviour
{
    private float npcForce = 10f;
    private float playerForce = 30f;

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
            HandleNpcHit(navMeshAgent, directionOfForce);
            return;
        }

        // Handles hitting the player
        HandlePlayerHit(entity, directionOfForce);
    }

    private void HandleNpcHit(NavMeshAgent navMeshAgent, Vector3 directionOfForce)
    {
        Vector3 forceVector = directionOfForce * npcForce;
        navMeshAgent.velocity += forceVector;
        StartCoroutine(DestroyGameObject());
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
