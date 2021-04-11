using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public override string entityPrefabType { get => "FPSController"; }

    private void Awake()
    {
        base.Awake();
        EntityUniqueIdentifier = "Player";
    }
}
