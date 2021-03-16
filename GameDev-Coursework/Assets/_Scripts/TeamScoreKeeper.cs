using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class TeamScoreKeeper : MonoBehaviour
{
    private int redTeamScore = 0;
    private int blueTeamScore = 0;

    [SerializeField]
    public int RedTeamScore
    {
        get { return redTeamScore; }

        set
        {
            redTeamScore = value;
            UpdateScoreOnUI();
        }
    }

    [SerializeField]
    public int BlueTeamScore
    {
        get { return blueTeamScore; }

        set
        {
            blueTeamScore = value;
            UpdateScoreOnUI();
        }
    }

    public void UpdateTeamScoreWithKill(TeamScoreTypes teamScoreTypes)
    {
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
                break;
            case TeamScoreTypes.BlueTeamSacrifice:
                BlueTeamScore += 20;
                break;
        }
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
                    UpdateScoreText(child.GetComponent<TMPro.TextMeshProUGUI>(), RedTeamScore);
                }

                if(childTag == GameConstants.BLUE_TEAM_SCORE_TEXT_TAG)
                {
                    UpdateScoreText(child.GetComponent<TMPro.TextMeshProUGUI>(), BlueTeamScore);
                }

                if (childTag == GameConstants.RED_TEAM_SCORE_BAR_TAG)
                {
                    UpdateScoreBar(child.GetComponent<Image>(), RedTeamScore);
                }

                if (childTag == GameConstants.BLUE_TEAM_SCORE_BAR_TAG)
                {
                    UpdateScoreBar(child.GetComponent<Image>(), BlueTeamScore);
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
}

public enum TeamScoreTypes
{
    RedTeamKill,
    BlueTeamKill,
    RedTeamSacrifice,
    BlueTeamSacrifice
}
