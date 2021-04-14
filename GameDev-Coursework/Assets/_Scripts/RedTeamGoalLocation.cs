// Adds points to BLUE team
public class RedTeamGoalLocation : TeamGoalLocation
{
    protected override void AddPointToTeam(Entity entity)
    {
        // Add points to blue team
        if (entity.teamType == TeamType.BLUE_TEAM)
        {
            teamScoreKeeper.UpdateTeamScoreWithKill(TeamScoreTypes.BlueTeamSacrifice, entity);
            entityRespawner.ReSpawnEntity(entity);
        }
    }
}
