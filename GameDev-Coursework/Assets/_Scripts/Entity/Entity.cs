using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    public string EntityUniqueIdentifier { get; protected set; }
    public TeamGoalLocation goalToScoreIn { get; private set; }
    public abstract string entityPrefabType { get; }
    public bool isRedTeam = true;
    public TeamType teamType;
    public Vector3 originalScale { get; private set; }
    protected NavMeshAgent navMeshAgent;
    private Entity entityShotMeLast;

    public AudioSource deathSound;

    protected void Awake()
    {
        originalScale = transform.localScale;
        SetGoalLocation();
    }

    public void UpdateEntityThatShotMeLast(Entity entity)
    {
        entityShotMeLast = entity;
    }

    private void SetGoalLocation()
    {
        string goalTag = GameConstants.RED_TEAM_GOAL;

        if(teamType == TeamType.RED_TEAM)
            goalTag = GameConstants.BLUE_TEAM_GOAL;

        goalToScoreIn = GameObject.FindGameObjectWithTag(goalTag).GetComponent<TeamGoalLocation>();
    }

    public abstract void SpecialisedSpawnHelper();

    public IEnumerator SpawnResizer()
    {
        transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        yield return new WaitForSeconds(1);
        transform.localScale = originalScale;
        SpecialisedSpawnHelper();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }
}

public enum TeamType
{
    RED_TEAM,
    BLUE_TEAM
}
