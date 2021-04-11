using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LoadGameController loadGameController { get; private set; }
    private static GameManager gameManagerInstance;

    private Scene activeScene;
    private EntityLoadingHandler entityLoadingHandler;

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
    }

    // Game manager needs to:
    // - Store active scene
    // - Persist accross scenes
    // - Switch scenes
    // - Be a singleton
    // - Save the game state
    // - Load the game state

    // Need to implement spawn points before saving and loading

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

        Camera mainCamera = Camera.main;

    }
}
