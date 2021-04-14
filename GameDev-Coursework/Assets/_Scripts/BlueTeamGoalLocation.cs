// Adds points to RED team
public class BlueTeamGoalLocation : TeamGoalLocation
{
    protected override void AddPointToTeam(Entity entity)
    {
        // Add points to red team
        if (entity.teamType == TeamType.RED_TEAM)
        {
            teamScoreKeeper.UpdateTeamScoreWithKill(TeamScoreTypes.RedTeamSacrifice, entity);
            entityRespawner.ReSpawnEntity(entity);
        }
    }
}
