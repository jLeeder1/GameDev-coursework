using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : MonoBehaviour
{
    private BulletFactory bulletFactory;

    [SerializeField]
    private float fireRate; // smaller is faster

    private bool canFireBullet;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        bulletFactory = GetComponent<BulletFactory>();
        canFireBullet = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canFireBullet)
        {
            bulletFactory.InstantiateBullet();
            StartCoroutine(CanFireBullet());
            audioSource.Play();
        }
    }

    IEnumerator CanFireBullet()
    {
        canFireBullet = false;
        yield return new WaitForSeconds(fireRate);
        canFireBullet = true;
    }
}
