using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private const int mainMenuIndex = 0;
    private const int singlePlayerMenuIndex = 1;
    private const int lavaLevelIndex = 2;

    public IEnumerator LoadScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void MainMenuSinglePlayerToSinglePlayerMenu()
    {
        Debug.Log("Single player pressed");

        StartCoroutine(LoadScene(singlePlayerMenuIndex));
    }

    public void MainMenuMultiPlayerToMultiplayer()
    {
        Debug.Log("Multi player pressed");
    }

    public void SinglePlayerToMainMenu()
    {
        Debug.Log("Single player to main menu");

        StartCoroutine(LoadScene(mainMenuIndex));
    }

    public void SinglePlayerToLavaLevel()
    {
        Debug.Log("Single player to lava level");

        StartCoroutine(LoadScene(lavaLevelIndex));
    }

    public void SinglePlayerToSecondLevel()
    {
        Debug.Log("Single player to second level");
    }

    public void SinglePlayerToThirdLevel()
    {
        Debug.Log("Single player to third level");
    }
}
