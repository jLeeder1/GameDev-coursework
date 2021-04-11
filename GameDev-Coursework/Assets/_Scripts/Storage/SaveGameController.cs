using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameController : MonoBehaviour
{
    public void SaveGame()
    {
        Debug.Log("SaveGame Entered");
        string saveName = GetSaveFileName();

        GameState gameState = CreateGameState();
        XmlDocument xmlDocument = new XmlDocument();
        XmlSerializer serializer = new XmlSerializer(typeof(GameState));
        using (MemoryStream stream = new MemoryStream())
        {
            serializer.Serialize(stream, gameState);
            stream.Position = 0;
            xmlDocument.Load(stream);
            xmlDocument.Save(saveName);
        }

        Debug.Log("SaveGame Finished");
    }

    private GameState CreateGameState()
    {
        EntitySaveState[] entitySaveStates = GetAllEnitySaveStates();

        return new GameState(entitySaveStates);
    }

    private EntitySaveState[] GetAllEnitySaveStates()
    {
        EntitySaveObject[] saveObjects = GameObject.FindObjectsOfType<EntitySaveObject>();
        EntitySaveState[] entitySaveStates = new EntitySaveState[saveObjects.Length];

        for(int index = 0; index < saveObjects.Length; index++)
        {
            entitySaveStates[index] = saveObjects[index].CreateEntitySaveState();
        }

        return entitySaveStates;
    }

    private string GetSaveFileName()
    {
        Scene scene = SceneManager.GetActiveScene();

        return SaveFileHelper.GetFileNameFromSceneName(scene.name);
    }
}
