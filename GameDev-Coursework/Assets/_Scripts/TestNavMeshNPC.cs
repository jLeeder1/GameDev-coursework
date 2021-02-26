using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavMeshNPC : MonoBehaviour
{
    [SerializeField]
    GameObject targetGameObject;

    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(targetGameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
