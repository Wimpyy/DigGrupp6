using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float shootSpeed;
    [SerializeField] float bulletDistance;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;

    float fireRateTimer;

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        LookAtMouse();

        if (Input.GetMouseButton(0))
        {
            Shoot(bulletPrefab, 1, shootSpeed, bulletDistance, .1f, 1f);
        }
    }

    void LookAtMouse()
    {
        var lookAtPos = Input.mousePosition;
        lookAtPos.z = transform.position.z - Camera.main.transform.position.z;
        lookAtPos = Camera.main.ScreenToWorldPoint(lookAtPos);
        transform.forward = lookAtPos - transform.position;
    }

    void Shoot(GameObject bulletPref, int bulletAmt, float bulletSpeed, float bulletLifeTime, float fireRate, float bulletSpread)
    {
        if (fireRateTimer < fireRate)
        {
            return;
        }
        for (int i = 0; i < bulletAmt; i++)
        {
            fireRateTimer = 0;
            GameObject bullet = Instantiate(bulletPref, shootPoint);
            bullet.transform.parent = null;

            Rigidbody bulletRb = bullet.AddComponent<Rigidbody>();
            bulletRb.useGravity = false;
            bulletRb.velocity = transform.forward * bulletSpeed + new Vector3(0, Random.Range(-bulletSpread, bulletSpread), 0);
            Destroy(bullet, bulletLifeTime);
        }
    }

}
