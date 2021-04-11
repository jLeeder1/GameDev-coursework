using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int buildIndex)
    {
        GameManager.GameManagerInstance.LoadFromBuildIndex(buildIndex);
    }

    public void LoadSceneByFile(int buildIndex)
    {
        GameManager.GameManagerInstance.LoadFromSaveFile(buildIndex);
    }
}
