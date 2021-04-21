using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviourHandler : MonoBehaviour
{
    private List<NPCBehaviourStateBase> behaviourStateBases;
    private int behaviourCooldownInSeconds = 4;
    private int randomChanceThreshold = 7;
    private NPCBehaviourStateBase NPCBehaviourStateBase;
    private Vector3 Target;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        behaviourStateBases = new List<NPCBehaviourStateBase>
        {
            GetComponent<NPCGoalSeekingBehaviour>(),
            GetComponent<NPCPlayerSeekingBehaviour>(),
            GetComponent<NPCPatrolBehaviour>()
        };

        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(PerformBehaviours());
        //Debug.Log("Turn on NPCBehaviourHandler.cs");
    }

    IEnumerator PerformBehaviours()
    {
        while (true)
        {
            for(int index = 0; index < behaviourStateBases.Count; index++)
            {
                if (behaviourStateBases[index].ShouldTriggerBehaviour() || BehaviourRandomChance())
                {
                    behaviourStateBases[index].PerformBehaviour();
                    index = 0; // To always iterate from the beginning
                    Target = behaviourStateBases[index].CurrentDestination;
                    transform.LookAt(Target);
                    yield return new WaitForSeconds(behaviourCooldownInSeconds);
                }
            }
        }
    }

    private void Update()
    {
        Debug.Log($"Look at: {Target}");
    }

    private bool BehaviourRandomChance()
    {
        int myRandomChance = Random.Range(0, 11);

        if (myRandomChance > randomChanceThreshold)
            return true;

        return false;
    }
}
