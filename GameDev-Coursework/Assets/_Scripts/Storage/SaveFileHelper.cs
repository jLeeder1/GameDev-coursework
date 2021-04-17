using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveFileHelper
{
    private static Dictionary<int, string> indexToNameDictionary = new Dictionary<int, string>()
    {
        {2, "LavaLevel" },
        {4, "MountainLevel" }
    };

    public static string GetFileNameFromSceneName(string sceneName)
    {
        return $"Assets/Saves/{sceneName}.xml";
    }

    public static Scene GetSceneFromName(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName);
    }

    public static string GetLevelNameFromBuildIndex(int buildIndex)
    {
        if (indexToNameDictionary.ContainsKey(buildIndex))
            return indexToNameDictionary[buildIndex];

        Debug.LogError(new KeyNotFoundException());
        Debug.Log("Build index not found in SaveFileHelper.cs. Defaulting to LavaLevel");

        return "LavaLevel";
    }
}
