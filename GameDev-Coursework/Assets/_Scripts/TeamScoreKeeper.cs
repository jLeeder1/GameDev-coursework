using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class TeamScoreKeeper : MonoBehaviour
{
    private int redTeamScore = 0;
    private int blueTeamScore = 0;
    private int playerScore = 0;
    private int maximumScore = 100;

    [SerializeField]
    public int RedTeamScore
    {
        get { return redTeamScore; }

        private set
        {
            redTeamScore = value;
            UpdateScoreOnUI();
        }
    }

    [SerializeField]
    public int BlueTeamScore
    {
        get { return blueTeamScore; }

        private set
        {
            blueTeamScore = value;
            UpdateScoreOnUI();
        }
    }

    public void UpdateTeamScoreWithKill(TeamScoreTypes teamScoreTypes, Entity entity)
    {

        string prefabType = entity.entityPrefabType;
        int scoreToAdd = 1;

        switch (teamScoreTypes)
        {
            case TeamScoreTypes.RedTeamKill:
                RedTeamScore += 1;
                break;
            case TeamScoreTypes.BlueTeamKill:
                BlueTeamScore += 1;
                break;
            case TeamScoreTypes.RedTeamSacrifice:
                RedTeamScore += 20;
                scoreToAdd = 20;
                break;
            case TeamScoreTypes.BlueTeamSacrifice:
                BlueTeamScore += 20;
                scoreToAdd = 20;
                break;
        }

        if (prefabType.Equals(GameConstants.PLAYER_PREFAB_SUFFIX))
        {
            Player player = (Player)entity;
            player.PlayerScore += scoreToAdd;
            playerScore += scoreToAdd;
        }

        UpdateScoreOnUI();
        CheckEndGametrigger();
    }

    public void UpdateScoreOnUI()
    {
        foreach(FirstPersonController firstPersonController in GameObject.FindObjectsOfType<FirstPersonController>())
        {
            Canvas canvas = firstPersonController.GetComponentInChildren<Canvas>();
            int childCount = canvas.transform.childCount;

            foreach (Transform child in canvas.transform)
            {
                string childTag = child.tag;

                if(childTag.Equals(GameConstants.RED_TEAM_SCORE_TEXT_TAG))
                {
                    this.UpdateScoreText(child.GetComponent<TMPro.TextMeshProUGUI>(), RedTeamScore);
                }

                if(childTag == GameConstants.BLUE_TEAM_SCORE_TEXT_TAG)
                {
                    this.UpdateScoreText(child.GetComponent<TMPro.TextMeshProUGUI>(), BlueTeamScore);
                }

                if (childTag == GameConstants.RED_TEAM_SCORE_BAR_TAG)
                {
                    this.UpdateScoreBar(child.GetComponent<Image>(), RedTeamScore);
                }

                if (childTag == GameConstants.BLUE_TEAM_SCORE_BAR_TAG)
                {
                    this.UpdateScoreBar(child.GetComponent<Image>(), BlueTeamScore);
                }
            }
        }
    }

    private void UpdateScoreText(TMPro.TextMeshProUGUI textComponent, int score)
    {
        textComponent.text = score.ToString();
    }

    private void UpdateScoreBar(Image imageComponent, int score)
    {
        imageComponent.fillAmount = (float)score / GameConstants.SCORE_LIMIT;
    }

    private void CheckEndGametrigger()
    {
        if(redTeamScore >= maximumScore || blueTeamScore >= maximumScore)
        {
            StartCoroutine(UpdateBackendlessHighScoreAndMoveToExitScreen(playerScore));
        }
    }

    IEnumerator UpdateBackendlessHighScoreAndMoveToExitScreen(int newHighScore)
    {
        bool hasBackendlessScoreUpdated = false;

        if (playerScore > GameManager.GameManagerInstance.highScore)
        {
            StartCoroutine(RemoteHighScoreManager.Instance.CreateHighScoreCR(newHighScore));

            while (!hasBackendlessScoreUpdated)
            {
                yield return null;

                if (RemoteHighScoreManager.Instance.hasUpdatedScore)
                {
                    hasBackendlessScoreUpdated = true;
                    RemoteHighScoreManager.Instance.hasUpdatedScore = false;
                    GameManager.GameManagerInstance.highScore = newHighScore;
                }
            }
        }

        int winningScore = Mathf.Max(blueTeamScore, redTeamScore);
        bool isRedTeamWinner = IsRedTeamWinner();
        ScoreStructure scoreStructure = new ScoreStructure(blueTeamScore, redTeamScore, playerScore, winningScore, isRedTeamWinner);
        GameManager.GameManagerInstance.HandleEndGame(scoreStructure);
    }

    public ScoreStructureSaveState CreateScoreStrucutreStateForSaveGame()
    {
        return new ScoreStructureSaveState(blueTeamScore, redTeamScore, playerScore);
    }

    public void LoadScoresFromFile(int blueTeamScore, int redTeamScore, int playerScore)
    {
        this.blueTeamScore = blueTeamScore;
        this.redTeamScore = redTeamScore;
        this.playerScore = playerScore; // Don;t think i need to add score directly to player as this will be done upon them scoring a point
        UpdateScoreOnUI();
    }

    private bool IsRedTeamWinner()
    {
        if(redTeamScore > blueTeamScore)
        {
            return true;
        }

        return false;
    }
}

public enum TeamScoreTypes
{
    RedTeamKill,
    BlueTeamKill,
    RedTeamSacrifice,
    BlueTeamSacrifice
}
