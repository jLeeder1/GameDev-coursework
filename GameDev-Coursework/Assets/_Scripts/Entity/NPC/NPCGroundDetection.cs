using UnityEngine;
using UnityEngine.AI;

public class NPCGroundDetection : MonoBehaviour
{
    public DetectGroundWithRay[] detectGroundWithRaysInChildren { get; private set; }
    private int limitOfRaysHittingGround = 2;
    private int numberOfRaysHittingTheGround = 0;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        detectGroundWithRaysInChildren = GetComponentsInChildren<DetectGroundWithRay>();
    }

    private void FixedUpdate()
    {
        DetectNumberOfRaysOutOfBounds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameConstants.BULLET_TAG)
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
        if (navMeshAgent.enabled == false)
        {
            return;
        }

        int numOfRaysHittingGround = 0;

        foreach (DetectGroundWithRay detectGroundWithRay in detectGroundWithRaysInChildren)
        {
            if (detectGroundWithRay.IsRayHittingGround())
            {
                numOfRaysHittingGround++;
            }
        }

        numberOfRaysHittingTheGround = numOfRaysHittingGround;
    }
}
