using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField]
    private LayerMask NPC;

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
            teamScoreKeeper.UpdateTeamScoreWithKill(GetTeamScoreToUpdate(collision.gameObject.GetComponent<TestNavMeshNPC>()));
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
}
