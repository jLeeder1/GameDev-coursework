using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HighScoreResultCallback : UnityEvent<HighScoreResult>
{
}

[Serializable]
public class HighScoreResult
{
    public int Score;
    public string code;// error code
    public string message; // error message
    public string objectId;
}

[Serializable]
public class HighScoreRequest
{
    public int Score;
}

public class RemoteHighScoreManager : MonoBehaviour
{
    public static RemoteHighScoreManager Instance { get; private set; }

    public HighScoreResultCallback OnHighScoreGet = new HighScoreResultCallback();
    public bool hasUpdatedScore { get; set; }

    [SerializeField]
    private Text highScoreText;

    void Awake()
    {
        // force singleton instance
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        // do not destroy this object when we load the scene
    }

    public IEnumerator CreateHighScoreCR(int newHighScore)
    {
        HighScoreRequest highScoreRequest = new HighScoreRequest();
        highScoreRequest.Score = newHighScore;

        //string requestUri = $"https://eu-api.backendless.com/{BackendlessStorageInfo.APPLICATION_ID}/{BackendlessStorageInfo.REST_SECRET_KEY}/data/HighScore";
        string requestUri = $"https://eu-api.backendless.com/{BackendlessStorageInfo.APPLICATION_ID}/{BackendlessStorageInfo.REST_SECRET_KEY}/data/HighScore/{BackendlessStorageInfo.HIGH_SCORE_ID}";
        UnityWebRequest unityWebRequest = UnityWebRequest.Put(requestUri, JsonUtility.ToJson(highScoreRequest));
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isNetworkError)
        {
            highScoreText.text = BackendlessStorageInfo.NETWORK_ERROR_MESSAGE;
        }
        else
        {
            HighScoreResult result = JsonUtility.FromJson<HighScoreResult>(unityWebRequest.downloadHandler.text);
            PlayerPrefs.SetString("HighScoreObjectId", result.objectId);
        }

        hasUpdatedScore = true;
    }

    public IEnumerator GetHighScoreCR(Action<int> OnCompleteCallback)
    {
        string requestUri = $"https://eu-api.backendless.com/{BackendlessStorageInfo.APPLICATION_ID}/{BackendlessStorageInfo.REST_SECRET_KEY}/data/HighScore/{BackendlessStorageInfo.HIGH_SCORE_ID}";
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(requestUri);
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isNetworkError)
        {
            highScoreText.text = BackendlessStorageInfo.NETWORK_ERROR_MESSAGE;
        }
        else
        {
            HighScoreResult result = JsonUtility.FromJson<HighScoreResult>(unityWebRequest.downloadHandler.text);
            GameManager.GameManagerInstance.highScore = result.Score;
            OnCompleteCallback.Invoke(result.Score);
        }
    }
}
