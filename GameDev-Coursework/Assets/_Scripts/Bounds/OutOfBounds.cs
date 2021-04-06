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
            NPC npc = collision.gameObject.GetComponent<NPC>();
            teamScoreKeeper.UpdateTeamScoreWithKill(GetTeamScoreToUpdate(npc));
            ReSpawnNPC(npc);
        }
    }

    private TeamScoreTypes GetTeamScoreToUpdate(NPC testNavMeshNPC)
    {
        if (testNavMeshNPC.isRedTeam)
        {
            return TeamScoreTypes.RedTeamKill;
        }

        return TeamScoreTypes.BlueTeamKill;
    }

    private void ReSpawnNPC(NPC NPC)
    {
        string spawnPointTag = GameConstants.BLUE_TEAM_SPAWN_POINT;

        if (NPC.isRedTeam)
        {
            spawnPointTag = GameConstants.RED_TEAM_SPAWN_POINT;
        }

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnPointTag);

        Transform spawnPointTransform = spawnPoints[Random.Range(0, spawnPoints.Length)].GetComponent<Transform>();

        NPC.transform.position = spawnPointTransform.position;
        NPC.nPCGroundDetection.ToggleAllDetectGroundWithRayInChildren(true);
        NPC.GetComponent<NavMeshAgent>().enabled = true;
        NPC.GetComponent<NavMeshAgent>().velocity = new Vector3(0f, 0f, 0f);
        NPC.GetComponent<NavMeshAgent>().SetDestination(testTarget.transform.position);
    }
}
