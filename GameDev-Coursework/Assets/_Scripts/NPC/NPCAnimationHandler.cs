using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationHandler : MonoBehaviour
{
    public bool IsOnOffMeshLink { get; private set; }

    private NavMeshAgent navMeshAgent;
    private float currentSpeed;

    // Animations
    private Animator animator;
    private int walkingSpeedAnimatorId;
    private int isJumpingUpId;
    private int jumpAnimatorId;

    // Cool down
    private int isJumpingUpCoolDownTimer = 2;
    private bool isJumpingUpCoolDownActive = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoTraverseOffMeshLink = false;
        animator = GetComponent<Animator>();
        SetAnimatorParamaters();
    }

    private void SetAnimatorParamaters()
    {
        walkingSpeedAnimatorId = Animator.StringToHash("walkingSpeed");
        isJumpingUpId = Animator.StringToHash("isJumpingUp");
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(walkingSpeedAnimatorId, currentSpeed);

        if (!navMeshAgent.isOnOffMeshLink)
        {
            return;
        }

        HandleNPCJumpingUpLedge();
        //IsNavMeshAgentOnNavMeshOffLink();
    }

    private void IsNavMeshAgentOnNavMeshOffLink()
    {
        animator.SetBool(isJumpingUpId, navMeshAgent.isOnOffMeshLink);
    }

    private void HandleNPCJumpingUpLedge()
    {
        if(navMeshAgent.currentOffMeshLinkData.endPos.y > navMeshAgent.currentOffMeshLinkData.startPos.y && !isJumpingUpCoolDownActive)
        {
            animator.SetBool(isJumpingUpId, true);
        }
    }

    public void CompleteOffNavMeshLink()
    {
        navMeshAgent.CompleteOffMeshLink();
    }

    public void ResetIsJumpingUp()
    {
        Debug.Log("Reset is jumping up test");
        animator.SetBool(isJumpingUpId, false);
        StartCoroutine(IsJumpingUpCoolDown());
    }

    IEnumerator IsJumpingUpCoolDown()
    {
        isJumpingUpCoolDownActive = true;
        yield return new WaitForSeconds(isJumpingUpCoolDownTimer);
        isJumpingUpCoolDownActive = false;
    }
}
