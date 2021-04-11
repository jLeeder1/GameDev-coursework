public class Player : Entity
{
    public override string entityPrefabType { get => "FPSController"; }

    protected void Awake()
    {
        base.Awake();
        EntityUniqueIdentifier = "Player";
    }
}
