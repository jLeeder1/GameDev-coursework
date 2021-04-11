using System.Collections.Generic;

public class NPCSaveState : EntitySaveObject
{
    private string uniqueIdentifier;

    protected void Start()
    {
        base.Start();
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
            //entity.targetGameObject.tag,
        };

        return gameObjectReferences.ToArray();
    }
}
