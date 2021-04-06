using UnityEngine;

public class DetectGroundWithRay : MonoBehaviour
{
    public bool IsRayHittingGround()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.green);

        RaycastHit raycastHit;
        Physics.Raycast(transform.position, Vector3.down, out raycastHit, LayerMask.GetMask("outOfBounds"));

        if (raycastHit.collider.gameObject.CompareTag("outOfBounds"))
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red);
            //GetComponentInParent<NavMeshAgent>().enabled = false;
            return true;
        }

        return false;
    }
}
