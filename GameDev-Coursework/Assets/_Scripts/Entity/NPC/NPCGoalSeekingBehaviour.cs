using UnityEngine;

public class NPCGoalSeekingBehaviour : NPCBehaviourStateBase
{
    private float distanceToRunTowardsGoal;

    protected void Awake()
    {
        base.Awake();
        distanceToRunTowardsGoal = 35f;
    }

    public override void PerformBehaviour()
    {
        SetDestination(npc.goalToScoreIn.transform.position);
        transform.LookAt(npc.goalToScoreIn.transform.position);
    }

    public override bool ShouldTriggerBehaviour()
    {
        float distanceToGoal = Vector3.Distance(npc.goalToScoreIn.transform.position, transform.position);

        if (distanceToGoal < distanceToRunTowardsGoal)
            return true;

        return false;
    }
}

