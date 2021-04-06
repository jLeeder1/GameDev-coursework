using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    private Entity entityShotMeLast;

    public bool isRedTeam = true;
    protected NavMeshAgent navMeshAgent;


    private void Start()
    {

    }

    public void UpdateEntityThatShotMeLast(Entity entity)
    {
        entityShotMeLast = entity;
    }
}
