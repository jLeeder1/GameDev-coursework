using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationHandler : MonoBehaviour
{
    private readonly string npcVelocityAnimatorString = "npcVelocity";
    private int npcVelocityId;

    private float currentSpeed = 0;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    // Debugging
    private Vector3 facingDirection;

    void Start()
    {
        SetAnimatorIDs();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        facingDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(npcVelocityId, currentSpeed);
    }

    private void SetAnimatorIDs()
    {
        npcVelocityId = Animator.StringToHash(npcVelocityAnimatorString);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.color = Color.white;
    }
}
