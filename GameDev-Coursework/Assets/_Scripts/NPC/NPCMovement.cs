using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform currentTargetTransform;
    private string opposingTeamTag;
    private float stoppingDistance;
    private int updateTargetCoolDownInSeconds = 3;
    private bool isUpdateTargetCoolDownActive = false;
    private float proximityToLookAtTarget = 15f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetOpposingTeamTag();
        SetDestinationToRandomOpposingTeamMember(); // Will need to take this out of start
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUpdateTargetCoolDownActive)
        {
            SetDestinationToRandomOpposingTeamMember();
        }

        LookAtTargetWhenInProximity();

        Debug.DrawRay(transform.position, transform.forward, Color.white, 100000); // ONLY FOR DEBUGGING
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
        currentTargetTransform = GameObject.FindGameObjectWithTag(opposingTeamTag).transform;
        navMeshAgent.SetDestination(currentTargetTransform.position);
        StartCoroutine(UpdateTargetDestinationCoolDown());
    }

    private IEnumerator UpdateTargetDestinationCoolDown()
    {
        isUpdateTargetCoolDownActive = true;
        yield return new WaitForSeconds(updateTargetCoolDownInSeconds);
        isUpdateTargetCoolDownActive = false;
    }

    private void LookAtTargetWhenInProximity()
    {
        if(Vector3.Distance(transform.position, currentTargetTransform.position) >= proximityToLookAtTarget)
        {
            transform.LookAt(currentTargetTransform);
        }
    }
}
