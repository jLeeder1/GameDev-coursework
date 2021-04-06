using UnityEngine;

public class PlayerScoreTracker : MonoBehaviour
{
    [SerializeField]
    private int playerScore;

    public void UpdatePlayerScore(int score)
    {
        playerScore += score;
    }
}
