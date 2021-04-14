public struct GameState
{
    public EntitySaveState[] entityStates;
    public ScoreStructureSaveState scoreStructure;

    public GameState(EntitySaveState[] entityStates, ScoreStructureSaveState scoreStructure)
    {
        this.entityStates = entityStates;
        this.scoreStructure = scoreStructure;
    }
}
