using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCSaveState : EntitySaveObject
{
    private NPC entity;
    private string uniqueIdentifier;

    private void Start()
    {
        entity = GetComponent<NPC>();
        uniqueIdentifier = entity.EntityUniqueIdentifier;
    }

    public override EntitySaveState CreateEntitySaveState()
    {
        return new EntitySaveState(
            transform.position,
            transform.rotation,
            entityPrefabType,
            uniqueIdentifier,
            GetGameObjectTagReferences());
    }

    public override string[] GetGameObjectTagReferences()
    {
        List<string> gameObjectReferences = new List<string>()
        {
            entity.targetGameObject.tag,
        };

        return gameObjectReferences.ToArray();
    }
}
