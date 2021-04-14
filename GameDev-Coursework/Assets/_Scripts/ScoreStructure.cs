﻿using System;

public class ScoreStructure
{
    public int blueTeamScore { get; private set; }
    public int redTeamScore { get; private set; }
    public int playerScore { get; private set; }
    public int winningTeamScore { get; private set; }
    public bool isRedTeamWinner { get; private set; }

    public ScoreStructure(int blueTeamScore, int redTeamScore, int playerScore, int winningTeamScore, bool isRedTeamWinner)
    {
        this.blueTeamScore = blueTeamScore;
        this.redTeamScore = redTeamScore;
        this.playerScore = playerScore;
        this.winningTeamScore = winningTeamScore;
        this.isRedTeamWinner = isRedTeamWinner;
    }
}

[Serializable]
public struct ScoreStructureSaveState
{
    public int blueTeamScore;
    public int redTeamScore;
    public int playerScore;

    public ScoreStructureSaveState(int blueTeamScore, int redTeamScore, int playerScore)
    {
        this.blueTeamScore = blueTeamScore;
        this.redTeamScore = redTeamScore;
        this.playerScore = playerScore;
    }
}
