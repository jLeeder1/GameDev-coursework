using UnityEngine;

public class NPCPlayerSeekingBehaviour : NPCBehaviourStateBase
{
    private float distanceToChasePlayer;

    protected void Awake()
    {
        base.Awake();
        distanceToChasePlayer = 30f;
    }

    public override void PerformBehaviour()
    {
        SetDestination(npc.playerObject.transform.position);
        transform.LookAt(npc.playerObject.transform.position);
    }

    public override bool ShouldTriggerBehaviour()
    {
        Vector3 playerCurrentPosition = npc.playerObject.transform.position;
        float distanceToPlayer = Vector3.Distance(playerCurrentPosition, transform.position);

        if (distanceToPlayer < distanceToChasePlayer)
            return true;

        return false;
    }
}
