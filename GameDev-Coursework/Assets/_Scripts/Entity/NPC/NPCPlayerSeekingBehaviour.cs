using UnityEngine;

public class NPCPlayerSeekingBehaviour : NPCBehaviourStateBase
{
    [SerializeField]
    private NPCBulletFactory bulletFactory;

    private float distanceToChasePlayer;
    private float distanceToShootPlayer;

    protected void Awake()
    {
        base.Awake();
        distanceToChasePlayer = 40f;
    }

    public override void PerformBehaviour()
    {
        SetDestination(npc.playerObject.transform.position);
        transform.LookAt(npc.playerObject.transform.position);
        bulletFactory.InstantiateBullet(npc.playerObject.transform.position);
    }

    public override bool ShouldTriggerBehaviour()
    {
        Vector3 playerCurrentPosition = npc.playerObject.transform.position;
        float distanceToPlayer = Vector3.Distance(playerCurrentPosition, transform.position);

        if (distanceToPlayer < distanceToChasePlayer)
            return true;

        return false;
    }

    private void Update()
    {
        Vector3 playerCurrentPosition = npc.playerObject.transform.position;
        float distanceToPlayer = Vector3.Distance(playerCurrentPosition, transform.position);

        if(distanceToPlayer < distanceToShootPlayer)
        {
            transform.LookAt(npc.playerObject.transform.position);
            bulletFactory.InstantiateBullet(npc.playerObject.transform.position);
        }
    }
}
