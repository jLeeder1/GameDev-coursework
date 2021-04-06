using UnityEngine;
using UnityEngine.UI;

public class ScoreOnMenuController : MonoBehaviour
{
    public Text highScoreText;

    void Start()
    {
        StartCoroutine(RemoteHighScoreManager.Instance.GetHighScoreCR(UpdateUI));
    }

    void UpdateUI(int score)
    {
        if (score > 0) highScoreText.text = score.ToString();
        else highScoreText.text = "0 - Better work on this";
    }

    public void ButtonHandlerReset()
    {
        //StartCoroutine(RemoteHighScoreManager.Instance.SetHighScoreCR(0, ResetOnComplete));
    }

    void ResetOnComplete()
    {
        UpdateUI(0);
    }
}
