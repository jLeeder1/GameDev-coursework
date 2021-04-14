using UnityEngine;

public class EndGameHandler : MonoBehaviour
{
    public void HandleEndGame(ScoreStructure scoreStructure)
    {
        // Update player score
        UpdateScoreText(GameConstants.PLAYER_SCORE_TEXT_OBJECT, scoreStructure.playerScore);

        // Update winning team text
        GameObject scoreObject = GameObject.Find(GameConstants.WINNING_TEAM_TEXT_OBJECT);
        TextMesh textMesh = scoreObject.GetComponent<TextMesh>();
        UpdateColour(GameConstants.WINNING_TEAM_TEXT_OBJECT, scoreStructure.isRedTeamWinner, textMesh);

        // Update red team score
        UpdateScoreText(GameConstants.RED_TEAM_SCORE_Text_OBJECT, scoreStructure.redTeamScore);

        // Update blue team score
        UpdateScoreText(GameConstants.BLUE_TEAM_SCORE_TEXT_OBJECT, scoreStructure.blueTeamScore);
    }

    private void UpdateScoreText(string objectName, int scoreText)
    {
        GameObject scoreObject = GameObject.Find(objectName);
        TextMesh textMesh = scoreObject.GetComponent<TextMesh>();
        textMesh.text = scoreText.ToString();
    }

    private void UpdateColour(string objectName, bool isRedTeamWinner, TextMesh textMesh)
    {
        if (objectName.Equals(GameConstants.WINNING_TEAM_TEXT_OBJECT))
        {
            if (isRedTeamWinner)
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(GameConstants.RED_TEAM_COLOR, out color))
                {
                    textMesh.color = color;
                }
            }
            else
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(GameConstants.BLUE_TEAM_COLOR, out color))
                {
                    textMesh.color = color;
                }
            }
        }
    }
}
