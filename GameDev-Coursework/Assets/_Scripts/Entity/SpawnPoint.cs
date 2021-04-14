using UnityEngine;

public abstract class SpawnPoint : MonoBehaviour
{
    public abstract string SpawnPointTag { get; }

    [SerializeField]
    public float roationAroundSpawnAddition;

    protected void Awake()
    {
        gameObject.tag = SpawnPointTag;
    }
}
