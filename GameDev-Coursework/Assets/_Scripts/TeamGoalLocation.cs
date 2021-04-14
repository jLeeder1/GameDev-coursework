using UnityEngine;

public abstract class TeamGoalLocation : MonoBehaviour
{
    protected TeamScoreKeeper teamScoreKeeper;
    protected EntityRespawner entityRespawner;

    private void Start()
    {
        teamScoreKeeper = GameObject.FindObjectOfType<TeamScoreKeeper>();
        entityRespawner = gameObject.AddComponent<EntityRespawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Entity>();

        if (entity == null)
            return;

        AddPointToTeam(entity);
    }

    protected abstract void AddPointToTeam(Entity entity);
}
