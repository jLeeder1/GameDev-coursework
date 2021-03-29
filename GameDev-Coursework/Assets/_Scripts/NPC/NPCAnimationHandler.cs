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
    private int isJumpingDownId;

    // Cool down
    private int isJumpingUpCoolDownTimer = 2;
    private bool isJumpingUpCoolDownActive = false;
    private int isJumpingDownCoolDownTimer = 1;
    private bool isJumpingDownCoolDownActive = false;

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
        isJumpingDownId = Animator.StringToHash("isJumpingDown");
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
        HandleNPCJumpingDownLedge();
        //IsNavMeshAgentOnNavMeshOffLink();
    }

    private void IsNavMeshAgentOnNavMeshOffLink()
    {
        animator.SetBool(isJumpingUpId, navMeshAgent.isOnOffMeshLink);
    }

    private void HandleNPCJumpingUpLedge()
    {
        if(IsNextOffMeshLinkAboveCurrentLink() && !isJumpingUpCoolDownActive)
        {
            animator.SetBool(isJumpingUpId, true);
        }
    }

    private void HandleNPCJumpingDownLedge()
    {
        if (IsNextOffMeshLinkAboveCurrentLink() == false && !isJumpingDownCoolDownActive)
        {
            animator.SetBool(isJumpingDownId, true);
        }
    }

    private bool IsNextOffMeshLinkAboveCurrentLink()
    {
        if (navMeshAgent.currentOffMeshLinkData.endPos.y > navMeshAgent.currentOffMeshLinkData.startPos.y)
            return true;

        return false;
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

    public void ResetIsJumpingDown()
    {
        Debug.Log("Reset is jumping down test");
        animator.SetBool(isJumpingDownId, false);
        StartCoroutine(IsJumpingDownCoolDown());
    }

    IEnumerator IsJumpingUpCoolDown()
    {
        isJumpingUpCoolDownActive = true;
        yield return new WaitForSeconds(isJumpingUpCoolDownTimer);
        isJumpingUpCoolDownActive = false;
    }

    IEnumerator IsJumpingDownCoolDown()
    {
        isJumpingDownCoolDownActive = true;
        yield return new WaitForSeconds(isJumpingDownCoolDownTimer);
        isJumpingDownCoolDownActive = false;
    }
}
