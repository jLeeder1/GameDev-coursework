﻿using UnityEngine;
using UnityEngine.AI;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField]
    private LayerMask NPC;

    [SerializeField]
    private LayerMask PlayerLayerMask;

    [SerializeField]
    private GameObject testTarget;

    private TeamScoreKeeper teamScoreKeeper;
    private EntityRespawner entityRespawner;

    // Start is called before the first frame update
    void Start()
    {
        teamScoreKeeper = GameObject.FindObjectOfType<TeamScoreKeeper>();
        entityRespawner = gameObject.AddComponent<EntityRespawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((NPC.value & 1 << collision.gameObject.layer) != 0)
        {
            NPC npc = collision.gameObject.GetComponent<NPC>();
            teamScoreKeeper.UpdateTeamScoreWithKill(GetTeamScoreToUpdate(npc), npc);
            entityRespawner.ReSpawnEntity(npc);
        }

        if ((PlayerLayerMask.value & 1 << collision.gameObject.layer) != 0)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            teamScoreKeeper.UpdateTeamScoreWithKill(GetTeamScoreToUpdate(player), player);
            entityRespawner.ReSpawnEntity(player);
        }
    }

    private TeamScoreTypes GetTeamScoreToUpdate(Entity entity)
    {
        if (entity.teamType == TeamType.RED_TEAM)
        {
            return TeamScoreTypes.RedTeamKill;
        }

        return TeamScoreTypes.BlueTeamKill;
    }
}
