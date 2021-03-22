using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private string opposingTeamTag;
    private float stoppingDistance;
    private int updateTargetCoolDownInSeconds = 3;
    private bool isUpdateTargetCoolDownActive = false;

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
}
