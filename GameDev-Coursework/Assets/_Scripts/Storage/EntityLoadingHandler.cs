using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityLoadingHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject NPCPrefab;

    [SerializeField]
    private GameObject PlayerPrefab;

    private List<SpawnPoint> blueTeamSpawnPoints;
    private List<SpawnPoint> redTeamSpawnPoints;

    Vector3 redTeamLookAtOnSpawn;
    Vector3 blueTeamLookAtOnSpawn;

    private void Awake()
    {
        PlayerPrefab = (GameObject)Resources.Load($"{GameConstants.PREFAB_FOLDER_PREFIX}{GameConstants.PLAYER_PREFAB_SUFFIX}");
        NPCPrefab = (GameObject)Resources.Load($"{GameConstants.PREFAB_FOLDER_PREFIX}{GameConstants.NPC_PREFAB_SUFFIX}");
    }

    public GameObject CreateEntityFromSaveGame(EntitySaveState state)
    {
        GameObject entity = GameObject.Instantiate(GetPrefabFromString(state.EntityPrefabType), state.Position, state.Rotation);
        return entity;
    }

    public void SpawnTeams()
    {
        int playerCounter = 0;
        GetLevelSpawnPoints();

        // Spawn Player
        foreach (SpawnPoint blueTeamSpawnPoint in blueTeamSpawnPoints)
        {
            Vector3 spawnPosition = blueTeamSpawnPoint.transform.position;
            Quaternion rotation = GetRotationToLookAtOnSpawn(spawnPosition, blueTeamLookAtOnSpawn);

            if (playerCounter == 0)
            {
                SpawnSingleEntity(PlayerPrefab, spawnPosition, rotation);
                playerCounter++;
            }
            else
            {
                // Insert code for team NPC
            }
        }

        // Spawn red team entities
        foreach (SpawnPoint redTeamSpawnPoint in redTeamSpawnPoints)
        {
            Vector3 spawnPosition = redTeamSpawnPoint.transform.position;
            //Quaternion rotation = GetRotationToLookAtOnSpawn(spawnPosition, redTeamLookAtOnSpawn);

            SpawnSingleEntity(NPCPrefab, redTeamSpawnPoint.transform.position, Quaternion.identity);
        }
    }

    private Quaternion GetRotationToLookAtOnSpawn(Vector3 spawnPosition, Vector3 vectorToLookAt)
    {
        Vector3 direction = (vectorToLookAt - spawnPosition).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(direction);
        return lookDirection;
    }

    private GameObject GetPrefabFromString(string prefabType)
    {
        if (prefabType == "NPC")
        {
            return NPCPrefab;
        }

        if(prefabType == "FPSController")
        {
            return PlayerPrefab;
        }

        Debug.LogError("Error EntityLoadingHandler line 66. Could not find prefab type in state so using NPC as default");
        return NPCPrefab;
    }

    private List<SpawnPoint> GetListOfGivenSpawnPointTypes(string spawnPointTag)
    {
        List<GameObject> gameObjects = GameObject.FindGameObjectsWithTag(spawnPointTag).ToList();
        List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
        gameObjects.ForEach(x => spawnPoints.Add(x.GetComponent<SpawnPoint>()));

        return spawnPoints;
    }

    private GameObject SpawnSingleEntity(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = GameObject.Instantiate(prefab, position, rotation);
        //gameObject.transform.position = position;
        //gameObject.transform.rotation = rotation;
        //return GameObject.Instantiate(prefab, position, rotation);
        return gameObject;
    }

    private void GetLevelSpawnPoints()
    {
        blueTeamSpawnPoints = GetListOfGivenSpawnPointTypes(GameConstants.BLUE_TEAM_SPAWN_POINT);
        redTeamSpawnPoints = GetListOfGivenSpawnPointTypes(GameConstants.RED_TEAM_SPAWN_POINT);

        GameObject redTeamLookAtOnSpawnObj = GameObject.FindGameObjectWithTag(GameConstants.RED_TEAM_ON_SPAWN_LOOK_AT);
        GameObject blueTeamLookAtOnSpawnObj = GameObject.FindGameObjectWithTag(GameConstants.BLUE_TEAM_ON_SPAWN_LOOK_AT);

        redTeamLookAtOnSpawn = redTeamLookAtOnSpawnObj.transform.position;
        blueTeamLookAtOnSpawn = blueTeamLookAtOnSpawnObj.transform.position;
    }
}
