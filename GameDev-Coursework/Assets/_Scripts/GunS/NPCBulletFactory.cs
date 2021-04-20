using System.Collections;
using UnityEngine;

public class NPCBulletFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject bulletSpawn;

    [SerializeField]
    private float bulletThrust;

    [SerializeField]
    private float fireRate; // smaller is faster

    private bool canFireBullet = true;

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        fireRate = Random.Range(0.1f, 0.2f);
    }
    public void InstantiateBullet(Vector3 targetPosition)
    {
        if (!canFireBullet)
            return;

        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        GameObject bulleltInstance = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bulleltInstance.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(directionToTarget * bulletThrust);

        audioSource.Play();

        StartCoroutine(CanFireBullet());

    }

    IEnumerator CanFireBullet()
    {
        canFireBullet = false;
        yield return new WaitForSeconds(fireRate);
        canFireBullet = true;
    }

}
