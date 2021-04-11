using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    public string EntityUniqueIdentifier { get; protected set; }
    public TeamGoalLocation goalToScoreIn { get; private set; }
    public abstract string entityPrefabType { get; }
    public bool isRedTeam = true;
    protected NavMeshAgent navMeshAgent;
    private Entity entityShotMeLast;


    protected void Awake()
    {
        SetGoalLocation();
    }

    public void UpdateEntityThatShotMeLast(Entity entity)
    {
        entityShotMeLast = entity;
    }

    private void SetGoalLocation()
    {
        string goalTag = GameConstants.RED_TEAM_GOAL;

        if(isRedTeam)
            goalTag = GameConstants.BLUE_TEAM_GOAL;

        goalToScoreIn = GameObject.FindGameObjectWithTag(goalTag).GetComponent<TeamGoalLocation>();
    }
}
