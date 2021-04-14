using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class LoadGameController : MonoBehaviour
{
    [SerializeField]
    public EntityLoadingHandler entityLoadingHandler { get; private set; }

    private void Awake()
    {
        entityLoadingHandler = gameObject.AddComponent<EntityLoadingHandler>();
    }

    public void LoadSaveFile(string fileName)
    {
        ClearScene();
        GameState gameState = LoadState(fileName);
        Dictionary<string, GameObject> objects = CreateObjects(gameState);
        LoadScoreFromFile(gameState.scoreStructure);
    }

    private void LoadSceneFromRecord(GameState state)
    {
        ClearScene();
        //Dictionary<string, string> links = CreateLinks(state);
        //LinkObjects(objects, links);
    }

    private void ClearScene()
    {
        EntitySaveObject[] entitySaveObjects = FindObjectsOfType<EntitySaveObject>();
        for (int i = 0; i < entitySaveObjects.Length; i++)
            GameObject.Destroy(entitySaveObjects[i].gameObject);
    }

    private Dictionary<string, GameObject> CreateObjects(GameState state)
    {
        Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>(50);

        int playerEntityIndex = 0;

        // Find and spawn player first
        for (int i = 0; i < state.entityStates.Length; i++)
        {
            if(state.entityStates[i].EntityPrefabType == "FPSController")
            {
                playerEntityIndex = i;
                GameObject obj = entityLoadingHandler.CreateEntityFromSaveGame(state.entityStates[i]);
                Entity entity = obj.GetComponent<Entity>();
                objects.Add(entity.EntityUniqueIdentifier, obj);
            }
        }

        for (int i = 0; i < state.entityStates.Length; i++)
        {
            if (i == playerEntityIndex)
                continue;

            GameObject obj = entityLoadingHandler.CreateEntityFromSaveGame(state.entityStates[i]);
            Entity entity = obj.GetComponent<Entity>();
            objects.Add(entity.EntityUniqueIdentifier, obj);
        }
        return objects;
    }

    private GameState LoadState(string filename)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(filename);
        string xmlString = xmlDocument.OuterXml;

        GameState state;
        using (StringReader read = new StringReader(xmlString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameState));
            using (XmlReader reader = new XmlTextReader(read))
            {
                state = (GameState)serializer.Deserialize(reader);
            }
        }

        //LoadSceneFromRecord(state); was void return before
        return state;
    }

    private void LoadScoreFromFile(ScoreStructureSaveState scoreStructureSaveState)
    {
        TeamScoreKeeper teamScoreKeeper = GameObject.FindObjectOfType<TeamScoreKeeper>();
        teamScoreKeeper.LoadScoresFromFile(scoreStructureSaveState.blueTeamScore, scoreStructureSaveState.redTeamScore, scoreStructureSaveState.playerScore);
    }

    /*
    private Dictionary<string, string> CreateLinks(GameState gameState)
    {
        Dictionary<string, string> links = new Dictionary<string, string>(50);
        for (int i = 0; i < gameState.entityStates.Length; i++)
        {
            links.Add(gameState.entityStates[i].name, gameState.entityStates[i].targetName);
        }
        return links;
    }

    private void LinkObjects(Dictionary<string, GameObject> objects, Dictionary<string, string> links)
    {
        foreach (GameObject obj in objects.Values)
        {
            CubeController cont = obj.GetComponent<CubeController>();
            cont.Link(objects, links);
        }
    }
    */
}
