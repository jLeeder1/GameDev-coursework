using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssultRifle : MonoBehaviour
{
    private BulletFactory bulletFactory;

    [SerializeField]
    private float fireRate; // smaller is faster

    private bool canFireBullet;

    // Start is called before the first frame update
    void Start()
    {
        bulletFactory = GetComponent<BulletFactory>();
        canFireBullet = true;
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
        }
    }

    IEnumerator CanFireBullet()
    {
        canFireBullet = false;
        yield return new WaitForSeconds(fireRate);
        canFireBullet = true;
    }
}
