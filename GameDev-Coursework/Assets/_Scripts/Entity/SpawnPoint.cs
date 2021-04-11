using UnityEngine;

public abstract class SpawnPoint : MonoBehaviour
{
    public abstract string SpawnPointTag { get; }

    protected void Awake()
    {
        gameObject.tag = SpawnPointTag;
    }
}
