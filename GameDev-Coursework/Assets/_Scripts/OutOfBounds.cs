using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField]
    private LayerMask NPC;

    [SerializeField]
    private GameObject testTarget;

    private TeamScoreKeeper teamScoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        teamScoreKeeper = GameObject.FindObjectOfType<TeamScoreKeeper>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((NPC.value & 1 << collision.gameObject.layer) != 0)
        {
            TestNavMeshNPC npc = collision.gameObject.GetComponent<TestNavMeshNPC>();
            teamScoreKeeper.UpdateTeamScoreWithKill(GetTeamScoreToUpdate(npc));
            ReSpawnNPC(npc);
        }
    }

    private TeamScoreTypes GetTeamScoreToUpdate(TestNavMeshNPC testNavMeshNPC)
    {
        if (testNavMeshNPC.isRedTeam)
        {
            return TeamScoreTypes.RedTeamKill;
        }

        return TeamScoreTypes.BlueTeamKill;
    }

    private void ReSpawnNPC(TestNavMeshNPC testNavMeshNPC)
    {
        string spawnPointTag = GameConstants.BLUE_TEAM_SPAWN_POINT;

        if (testNavMeshNPC.isRedTeam)
        {
            spawnPointTag = GameConstants.RED_TEAM_SPAWN_POINT;
        }

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnPointTag);

        Transform spawnPointTransform = spawnPoints[Random.Range(0, spawnPoints.Length)].GetComponent<Transform>();

        testNavMeshNPC.transform.position = spawnPointTransform.position;
        testNavMeshNPC.ToggleAllDetectGroundWithRayInChildren(true);
        testNavMeshNPC.GetComponent<NavMeshAgent>().enabled = true;
        testNavMeshNPC.GetComponent<NavMeshAgent>().velocity = new Vector3(0f, 0f, 0f);
        testNavMeshNPC.GetComponent<NavMeshAgent>().SetDestination(testTarget.transform.position);
    }
}
