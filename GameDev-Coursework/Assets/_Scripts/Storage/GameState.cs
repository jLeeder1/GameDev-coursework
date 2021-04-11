using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameState
{
    public EntitySaveState[] entityStates;

    public GameState(EntitySaveState[] entityStates)
    {
        this.entityStates = entityStates;
    }
}
