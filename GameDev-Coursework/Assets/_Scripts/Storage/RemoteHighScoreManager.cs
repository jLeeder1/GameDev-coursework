using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

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

[Serializable]
public class HighScoreResultArray
{
    public HighScoreResult[] highScoreResults { get; set; }
}

public class RemoteHighScoreManager : MonoBehaviour
{
    public static RemoteHighScoreManager Instance { get; private set; }

    public HighScoreResultCallback OnHighScoreGet = new HighScoreResultCallback();

    void Awake()
    {
        // force singleton instance
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        // do not destroy this object when we load the scene
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator CreateHighScoreCR()
    {
        HighScoreRequest highScoreRequest = new HighScoreRequest();
        highScoreRequest.Score = 420;

        string requestUri = string.Format("https://eu-api.backendless.com/{0}/{1}/data/HighScore", Globals.APPLICATION_ID, Globals.REST_SECRET_KEY);
        UnityWebRequest unityWebRequest = UnityWebRequest.Put(requestUri, JsonUtility.ToJson(highScoreRequest));
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isNetworkError)
        {

        }
        else
        {
            HighScoreResult result = JsonUtility.FromJson<HighScoreResult>(unityWebRequest.downloadHandler.text);
            PlayerPrefs.SetString("HighScoreObjectId", result.objectId);
        }
    }

    public IEnumerator GetHighScoreCR(Action<int> OnCompleteCallback)
    {
        string requestUri = string.Format("https://eu-api.backendless.com/{0}/{1}/data/HighScore/{2}", Globals.APPLICATION_ID, Globals.REST_SECRET_KEY, "00C80F61-E44C-44E4-BF80-8201C294409C");
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(requestUri);
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isNetworkError)
        {

        }
        else
        {
            HighScoreResult result = JsonUtility.FromJson<HighScoreResult>(unityWebRequest.downloadHandler.text);
            OnCompleteCallback.Invoke(result.Score);
        }
    }
}
