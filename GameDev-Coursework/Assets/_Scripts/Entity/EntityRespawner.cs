using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityRespawner : MonoBehaviour
{
    private List<RedTeamSpawnPoint> redTeamSpawnPoints;
    private List<BlueTeamSpawnPoint> blueTeamSpawnPoints;
    Vector3 redTeamLookAtOnSpawn;
    Vector3 blueTeamLookAtOnSpawn;

    private void Start()
    {
        redTeamSpawnPoints = FindObjectsOfType<RedTeamSpawnPoint>().ToList();
        blueTeamSpawnPoints = FindObjectsOfType<BlueTeamSpawnPoint>().ToList();
        GameObject redTeamLookAtOnSpawnObj = GameObject.FindGameObjectWithTag(GameConstants.RED_TEAM_ON_SPAWN_LOOK_AT);
        GameObject blueTeamLookAtOnSpawnObj = GameObject.FindGameObjectWithTag(GameConstants.BLUE_TEAM_ON_SPAWN_LOOK_AT);

        redTeamLookAtOnSpawn = redTeamLookAtOnSpawnObj.transform.position;
        blueTeamLookAtOnSpawn = blueTeamLookAtOnSpawnObj.transform.position;
    }

    public void ReSpawnEntity(Entity entity)
    {
        SpawnPoint spawnPoint;
        Vector3 lookAtOnSpawn = redTeamLookAtOnSpawn;

        if (entity.teamType == TeamType.RED_TEAM)
        {
            spawnPoint = redTeamSpawnPoints[Random.Range(0, redTeamSpawnPoints.Count)];

        }
        else
        {
            spawnPoint = blueTeamSpawnPoints[Random.Range(0, blueTeamSpawnPoints.Count)];
            lookAtOnSpawn = blueTeamLookAtOnSpawn;
        }

        // Generic for all
        StartCoroutine(entity.SpawnResizer());
        Vector3 placeToSpawnEntity = spawnPoint.transform.position;
        placeToSpawnEntity.y += 2f; // Ensures they don't spawn in the ground
        entity.transform.position = placeToSpawnEntity;
        entity.transform.rotation = GetRotationToLookAtOnSpawn(placeToSpawnEntity, lookAtOnSpawn);
    }

    private Quaternion GetRotationToLookAtOnSpawn(Vector3 spawnPosition, Vector3 vectorToLookAt)
    {
        Vector3 direction = (vectorToLookAt - spawnPosition).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(direction);
        return lookDirection;
    }
}
