using System;
using UnityEngine;

public abstract class EntitySaveObject : MonoBehaviour
{
    public EntitySaveState entitySaveState;
    protected string entityPrefabType;
    protected Entity entity;

    protected void Start()
    {
        entity = GetComponent<Entity>();
        entityPrefabType = entity.entityPrefabType;
    }

    public abstract EntitySaveState CreateEntitySaveState();
    public abstract string[] GetGameObjectTagReferences();

}

[Serializable]
public struct EntitySaveState
{
    public Vector3 Position;
    public Quaternion Rotation;
    public string EntityPrefabType;
    public string Name;
    public string[] gameObjectTagReferences;

    public EntitySaveState(Vector3 position, Quaternion rotation, string entityPrefabType, string name, string[] gameObjectTagReferences)
    {
        this.Position = position;
        this.Rotation = rotation;
        EntityPrefabType = entityPrefabType;
        Name = name;
        this.gameObjectTagReferences = gameObjectTagReferences;
    }
}
