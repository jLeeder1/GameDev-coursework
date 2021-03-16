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
    private int numberOfRaysHittingTheGround = 0;
    private DetectGroundWithRay[] detectGroundWithRaysInChildren;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetGameObject.transform.position);

        detectGroundWithRaysInChildren = GetComponentsInChildren<DetectGroundWithRay>();
    }

    private void FixedUpdate()
    {
        DetectNumberOfRaysOutOfBounds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == GameConstants.BULLET_TAG)
        {
            if (numberOfRaysHittingTheGround >= limitOfRaysHittingGround)
            {
                navMeshAgent.enabled = false;
                ToggleAllDetectGroundWithRayInChildren(false);
            }
        }
    }

    public void ToggleAllDetectGroundWithRayInChildren(bool isEnabled)
    {
        foreach (DetectGroundWithRay detectGroundWithRay in detectGroundWithRaysInChildren)
        {
            detectGroundWithRay.enabled = isEnabled;
        }
    }

    private void DetectNumberOfRaysOutOfBounds()
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

        numberOfRaysHittingTheGround = numOfRaysHittingGround;
    }
}
