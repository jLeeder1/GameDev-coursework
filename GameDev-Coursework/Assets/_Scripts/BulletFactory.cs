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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            Vector3 relativePos = raycastHit.point - transform.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            GameObject bulleltInstance = Instantiate(bullet, bulletSpawn.transform.position, rotation);
            Rigidbody bulletRigidbody = bulleltInstance.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(ray.direction * bulletThrust);
        }
    }
}
