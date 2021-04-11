using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviourHandler : MonoBehaviour
{
    private List<NPCBehaviourStateBase> behaviourStateBases;
    private int behaviourCooldownInSeconds = 4;
    private int randomChanceThreshold = 7;

    private void Start()
    {
        behaviourStateBases = new List<NPCBehaviourStateBase>
        {
            GetComponent<NPCGoalSeekingBehaviour>(),
            GetComponent<NPCPlayerSeekingBehaviour>(),
            GetComponent<NPCPatrolBehaviour>()
        };

        StartCoroutine(PerformBehaviours());
    }

    IEnumerator PerformBehaviours()
    {
        while (true)
        {
            //for(NPCBehaviourStateBase nPCBehaviourStateBase in behaviourStateBases)
            for(int index = 0; index < behaviourStateBases.Count; index++)
            {
                if (behaviourStateBases[index].ShouldTriggerBehaviour() || BehaviourRandomChance())
                {
                    behaviourStateBases[index].PerformBehaviour();
                    index = 0; // To always iterate from the beginning
                    yield return new WaitForSeconds(behaviourCooldownInSeconds);
                }
            }
        }
    }

    private bool BehaviourRandomChance()
    {
        int myRandomChance = Random.Range(0, 11);

        if (myRandomChance > 7)
            return true;

        return false;
    }
}
