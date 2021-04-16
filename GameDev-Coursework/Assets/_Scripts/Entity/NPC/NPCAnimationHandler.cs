using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationHandler : MonoBehaviour
{
    public bool IsOnOffMeshLink { get; private set; }

    // Animator IDS
    private readonly string npcVelocityAnimatorString = "npcVelocity";
    private readonly string npcIsJumpingUpAnimatorString = "isJumping";
    private int npcVelocityId;
    private int isJumpingId;

    private float currentSpeed = 0;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    // Cool down
    private int isJumpingCoolDownTimer = 2;
    private bool isJumpingCoolDownActive = false;

    void Start()
    {
        SetAnimatorIDs();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = navMeshAgent.velocity.magnitude;
        animator.SetFloat(npcVelocityId, currentSpeed);

        if (!navMeshAgent.isOnOffMeshLink)
        {
            return;
        }

        HandleNPCJumpingUpLedge();
    }

    private void SetAnimatorIDs()
    {
        npcVelocityId = Animator.StringToHash(npcVelocityAnimatorString);
        isJumpingId = Animator.StringToHash(npcIsJumpingUpAnimatorString);
    }

    private void IsNavMeshAgentOnNavMeshOffLink()
    {
        animator.SetBool(isJumpingId, navMeshAgent.isOnOffMeshLink);
    }

    private void HandleNPCJumpingUpLedge()
    {
        /*
        if (navMeshAgent.currentOffMeshLinkData.endPos.y > navMeshAgent.currentOffMeshLinkData.startPos.y && !isJumpingCoolDownActive)
        {
            animator.SetBool(isJumpingId, true);
        }
        */

        if (navMeshAgent.isOnOffMeshLink && !isJumpingCoolDownActive)
        {
            animator.SetBool(isJumpingId, true);
        }
    }

    public void CompleteOffNavMeshLink()
    {
        navMeshAgent.CompleteOffMeshLink();
    }

    public void ResetIsJumpingUp()
    {
        Debug.Log("Reset is jumping up test");
        animator.SetBool(isJumpingId, false);
        StartCoroutine(IsJumpingUpCoolDown());
    }

    IEnumerator IsJumpingUpCoolDown()
    {
        isJumpingCoolDownActive = true;
        yield return new WaitForSeconds(isJumpingCoolDownTimer);
        isJumpingCoolDownActive = false;
    }
}
