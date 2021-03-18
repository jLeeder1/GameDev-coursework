using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private bool isMovementSetToChasePlayer;
    private NavMeshAgent navMeshAgent;
    private string opposingTeamTag;

    private float currentSpeed;
    private float stoppingDistance = 2f;

    private Animator animator;
    private int walkingSpeedAnimatorId;
    private int jumpAnimatorId;

    void Start()
    {
        isMovementSetToChasePlayer = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = stoppingDistance;
        GetOpposingTeamTag();
        SetDestinationToRandomOpposingTeamMember(); // Will need to take this out of start

        animator = GetComponent<Animator>();
        walkingSpeedAnimatorId = Animator.StringToHash("walkingSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(walkingSpeedAnimatorId, currentSpeed);
    }

    private void GetOpposingTeamTag()
    {
        opposingTeamTag = "redTeam";

        if(gameObject.tag == "redTeam")
        {
            opposingTeamTag = "blueTeam";
        }
    }

    private void SetDestinationToRandomOpposingTeamMember()
    {
        Transform targetTransform = GameObject.FindGameObjectWithTag(opposingTeamTag).transform;
        navMeshAgent.SetDestination(targetTransform.position);
    }
}
