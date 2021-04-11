using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    public string EntityUniqueIdentifier { get; protected set; }
    public abstract string entityPrefabType { get; }
    public bool isRedTeam = true;
    protected NavMeshAgent navMeshAgent;
    private Entity entityShotMeLast;

    protected void Awake()
    {

    }

    public void UpdateEntityThatShotMeLast(Entity entity)
    {
        entityShotMeLast = entity;
    }
}
