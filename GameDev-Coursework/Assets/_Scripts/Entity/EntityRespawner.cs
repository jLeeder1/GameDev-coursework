using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
            Vector3 placeToSpawnNPC = spawnPoint.transform.position;
            RespawnNPC(placeToSpawnNPC, entity, lookAtOnSpawn);
        }
        else
        {
            spawnPoint = blueTeamSpawnPoints[Random.Range(0, blueTeamSpawnPoints.Count)];
            Vector3 placeToSpawnPlayer = spawnPoint.transform.position;
            lookAtOnSpawn = blueTeamLookAtOnSpawn;
            RespawnPlayer(placeToSpawnPlayer, entity, lookAtOnSpawn);
        }

        // Generic for all
        StartCoroutine(entity.SpawnResizer());
    }

    private void RespawnPlayer(Vector3 positionToRespawn, Entity entity, Vector3 lookAtOnSpawn)
    {
        /*
        CharacterController characterController = entity.GetComponent<CharacterController>();
        FirstPersonController firstPersonController = entity.GetComponent<FirstPersonController>();

        characterController.enabled = false;
        firstPersonController.enabled = false;
        */

        entity.transform.position = positionToRespawn;
        entity.transform.rotation = GetRotationToLookAtOnSpawn(positionToRespawn, lookAtOnSpawn);
        /*
        characterController.enabled = true;
        firstPersonController.enabled = true;
        */
    }

    private void RespawnNPC(Vector3 positionToSpawn, Entity entity, Vector3 lookAtOnSpawn)
    {
        StartCoroutine(entity.SpawnResizer());
        Vector3 placeToSpawnEntity = positionToSpawn;
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
