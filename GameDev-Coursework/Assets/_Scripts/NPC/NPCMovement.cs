using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public bool IsOnOffMeshLink { get; private set; }
    private bool isMovementSetToChasePlayer;
    private NavMeshAgent navMeshAgent;
    private string opposingTeamTag;

    private float currentSpeed;
    private float stoppingDistance = 2f;
    private int updateTargetCoolDownInSeconds = 3;

    // Animations
    private Animator animator;
    private int walkingSpeedAnimatorId;
    private int isJumpingUpId;
    private int jumpAnimatorId;
    private bool isUpdateTargetCoolDownActive = false;

    void Start()
    {
        isMovementSetToChasePlayer = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = stoppingDistance;
        GetOpposingTeamTag();
        SetDestinationToRandomOpposingTeamMember(); // Will need to take this out of start

        animator = GetComponent<Animator>();
        walkingSpeedAnimatorId = Animator.StringToHash("walkingSpeed");
        isJumpingUpId = Animator.StringToHash("isJumpingUp");
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(walkingSpeedAnimatorId, currentSpeed);

        if (!isUpdateTargetCoolDownActive)
        {
            SetDestinationToRandomOpposingTeamMember();
        }

        IsNavMeshAgentOnNavMeshOffLink();
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
        StartCoroutine(UpdateTargetDestinationCoolDown());
    }

    private IEnumerator UpdateTargetDestinationCoolDown()
    {
        isUpdateTargetCoolDownActive = true;
        yield return new WaitForSeconds(updateTargetCoolDownInSeconds);
        isUpdateTargetCoolDownActive = false;
    }

    private void IsNavMeshAgentOnNavMeshOffLink()
    {
        animator.SetBool(isJumpingUpId, navMeshAgent.isOnOffMeshLink);
    }
}
