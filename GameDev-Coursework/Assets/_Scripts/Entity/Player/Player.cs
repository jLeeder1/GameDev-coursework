using UnityEngine;

public class Player : Entity
{
    public AudioSource musicAudioSource;

    public override string entityPrefabType { get => "FPSController"; }

    public int PlayerScore { get; set; }

    public override void SpecialisedSpawnHelper()
    {
        // Does nothing for now
    }

    protected void Awake()
    {
        teamType = TeamType.BLUE_TEAM;
        base.Awake();
        EntityUniqueIdentifier = "Player";
        isRedTeam = false;
        PlayerScore = 0;
    }

    public void UpdateMusicSpeed(float increaseAmount)
    {
        musicAudioSource.pitch += increaseAmount/150;
    }
}
