public class PlayerSaveState : EntitySaveObject
{
    protected void Start()
    {
        base.Start();
    }

    public override EntitySaveState CreateEntitySaveState()
    {
        return new EntitySaveState(
            transform.position,
            transform.rotation,
            entityPrefabType,
            GameConstants.PLAYER_TAG,
            GetGameObjectTagReferences());
    }

    public override string[] GetGameObjectTagReferences()
    {
        return new string[] { string.Empty };
    }
}
