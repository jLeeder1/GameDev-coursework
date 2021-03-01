using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BulletFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject bulletSpawn;

    [SerializeField]
    private float bulletThrust;

    private FirstPersonController firstPersonController;
    private Camera firstPersonControllerCamera;

    private void Start()
    {
        firstPersonController = GetComponentInParent<FirstPersonController>();
        firstPersonControllerCamera = GetComponentInParent<Camera>();
    }

    public void InstantiateBullet()
    {
        GameObject bulleltInstance = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        Rigidbody bulletRigidbody = bulleltInstance.GetComponent<Rigidbody>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray))
        {
            bulletRigidbody.AddForce(ray.direction * bulletThrust);
        }
    }
}
