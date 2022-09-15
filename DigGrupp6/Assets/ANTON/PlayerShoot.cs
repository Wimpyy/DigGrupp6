using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float shootSpeed;
    [SerializeField] float bulletDistance;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;

    GunClass gunClass;
    float fireRateTimer;

    private void Start()
    {
        gunClass = FindObjectOfType<GunClass>();
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        LookAtMouse();

        if (Input.GetMouseButton(0))
        {
            Shoot(gunClass.activeAmmo);

            gunClass.RefreshUI();
        }
    }

    void LookAtMouse()
    {
        var lookAtPos = Input.mousePosition;
        lookAtPos.z = transform.position.z - Camera.main.transform.position.z;
        lookAtPos = Camera.main.ScreenToWorldPoint(lookAtPos);
        transform.forward = lookAtPos - transform.position;
    }

    void Shoot(AmmoTypeClass activeAmmo)
    {
        if (fireRateTimer < activeAmmo.fireRate)
        {
            return;
        }
        for (int i = 0; i < activeAmmo.bulletAmmount; i++)
        {
            gunClass.activeAmmo.RemoveAmmo(1);
            fireRateTimer = 0;
            GameObject bullet = Instantiate(activeAmmo.bulletPrefab, shootPoint.transform.position, Quaternion.identity);
            bullet.transform.parent = null;

            Rigidbody bulletRb = bullet.AddComponent<Rigidbody>();
            bulletRb.useGravity = false;
            bulletRb.velocity = transform.forward * activeAmmo.bulletSpeed + new Vector3(0, Random.Range(-activeAmmo.bulletSpread, activeAmmo.bulletSpread), 0);
            Vector3 vel = bulletRb.velocity;

            bullet.transform.rotation = Quaternion.LookRotation(vel);

            Destroy(bullet, activeAmmo.bulletLifeTime);
        }
    }

}
