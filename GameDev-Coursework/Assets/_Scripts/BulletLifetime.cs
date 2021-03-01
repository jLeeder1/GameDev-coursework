using System.Collections;
using UnityEngine;

public class BulletLifetime : MonoBehaviour
{
    [SerializeField]
    private int bulletLifeInSeconds;

    void Start()
    {
        StartCoroutine(DestroyBulletAfterGivenTime());
    }

    IEnumerator DestroyBulletAfterGivenTime()
    {
        yield return new WaitForSeconds(bulletLifeInSeconds);
        Destroy(this.gameObject);
    }
}
