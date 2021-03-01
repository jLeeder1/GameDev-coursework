using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavMeshNPC : MonoBehaviour
{
    [SerializeField]
    GameObject targetGameObject;

    public bool isRedTeam = true;

    private NavMeshAgent navMeshAgent;

    private float timer = 0.2f;
    private int limitOfRaysHittingGround = 2;
    private DetectGroundWithRay[] detectGroundWithRaysInChildren;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetGameObject.transform.position);

        detectGroundWithRaysInChildren = GetComponentsInChildren<DetectGroundWithRay>();
    }

    private void FixedUpdate()
    {
        DetectIfRaysAreOutOfBounds();
    }

    private void DetectIfRaysAreOutOfBounds()
    {
        if(navMeshAgent.enabled == false)
        {
            return;
        }

        int numOfRaysHittingGround = 0;

        foreach(DetectGroundWithRay detectGroundWithRay in detectGroundWithRaysInChildren)
        {
            if (detectGroundWithRay.IsRayHittingGround())
            {
                numOfRaysHittingGround++;
            }
        }

        if(numOfRaysHittingGround >= limitOfRaysHittingGround)
        {
            navMeshAgent.enabled = false;
            ToggleAllDetectGroundWithRayInChildren(false);
        }
    }

    private void ToggleAllDetectGroundWithRayInChildren(bool isEnabled)
    {
        foreach (DetectGroundWithRay detectGroundWithRay in detectGroundWithRaysInChildren)
        {
            detectGroundWithRay.enabled = isEnabled;
        }
    }
}
