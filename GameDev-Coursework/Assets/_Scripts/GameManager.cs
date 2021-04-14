using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LoadGameController loadGameController { get; private set; }
    private static GameManager gameManagerInstance;

    private Scene activeScene;
    private EntityLoadingHandler entityLoadingHandler;
    private EndGameHandler endGameHandler;
    public ScoreStructure previousGameScoreStrucutre { get; private set; }
    public int highScore { get; set; }
    private int endGameSceneIndex = 3;
    private int mainMenuSceneIndex = 0;
    private int singlePlayerMenuSceneIndex = 1;

    public static GameManager GameManagerInstance
    {
        get
        {
            if (gameManagerInstance == null)
            {
                GameObject go = GameObject.Instantiate(new GameObject());
                gameManagerInstance = go.AddComponent<GameManager>();
            }
            return gameManagerInstance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        loadGameController = gameObject.AddComponent<LoadGameController>();
        entityLoadingHandler = gameObject.AddComponent<EntityLoadingHandler>();
        endGameHandler = gameObject.AddComponent<EndGameHandler>();
    }

    public Scene GetActiveScene() => activeScene;

    public void LoadFromBuildIndex(int buildIndex)
    {
        StartCoroutine(LoadScene(buildIndex, string.Empty));
    }

    public void LoadFromSaveFile(int buildIndex)
    {
        // Find scene index from file name
        string fileName = SaveFileHelper.GetLevelNameFromBuildIndex(buildIndex);
        StartCoroutine(LoadScene(buildIndex, fileName));
    }

    public IEnumerator LoadScene(int sceneIndex, string fileName = "")
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        activeScene = SceneManager.GetActiveScene();

        if(activeScene.buildIndex == endGameSceneIndex)
        {
            Cursor.lockState = CursorLockMode.Confined;
            endGameHandler.HandleEndGame(previousGameScoreStrucutre);
        }

        if(IsAMenuScene(activeScene.buildIndex))
        {
            yield break;
        }

        // If fileName is not empty then load from file
        if(!fileName.Equals(string.Empty))
        {
            string saveFileName = SaveFileHelper.GetFileNameFromSceneName(activeScene.name);
            loadGameController.LoadSaveFile(saveFileName);
        }
        // Else load game as normal
        else
        {
            entityLoadingHandler.SpawnTeams();
        }

        Camera mainCamera = Camera.main; // What is this for?

    }

    public void HandleEndGame(ScoreStructure scoreStructure)
    {
        previousGameScoreStrucutre = scoreStructure;

        // Set new high score in backendless
        if(scoreStructure.playerScore > highScore)
        {
            StartCoroutine(RemoteHighScoreManager.Instance.CreateHighScoreCR(scoreStructure.playerScore));
        }

        StartCoroutine(LoadScene(endGameSceneIndex));
    }

    private bool IsAMenuScene(int buildIndex)
    {
        if (buildIndex == mainMenuSceneIndex ||
            buildIndex == singlePlayerMenuSceneIndex ||
            buildIndex == endGameSceneIndex)
        {
            return true;
        }

        return false;
    }
}
