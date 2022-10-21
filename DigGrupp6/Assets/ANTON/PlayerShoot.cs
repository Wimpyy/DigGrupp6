using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float sniperChargeTime;
    [SerializeField] float sniperFovAdd;

    GunClass gunClass;
    float fireRateTimer;
    float sniperCharge = 0;

    float baseFov;
    bool cd = false;


    private void Start()
    {
        gunClass = FindObjectOfType<GunClass>();

        baseFov = cam.m_Lens.FieldOfView;
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        LookAtMouse();

        if (gunClass.activeAmmo.gunType == GunType.sniper)
        {

            if (Input.GetMouseButton(0) && !cd)
            {
                if (sniperCharge < sniperChargeTime)
                {
                    sniperCharge += Time.deltaTime;
                }

            }
            if (Input.GetMouseButtonUp(0) && sniperCharge >= sniperChargeTime - .3f)
            {
                Shoot(gunClass.activeAmmo);
                gunClass.RefreshUI();
                cd = true;
            }
            if (cd)
            {
                if (sniperCharge > 0)
                {
                    sniperCharge -= Time.deltaTime * 30;
                }
                else if (sniperCharge <= 0)
                {
                    cd = false;
                }
            }


            float chargePercent = sniperCharge / sniperChargeTime;
            cam.m_Lens.FieldOfView = baseFov + (sniperFovAdd * chargePercent);
        }

        if (Input.GetMouseButton(0) && gunClass.activeAmmo.gunType != GunType.sniper)
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
            if (activeAmmo.ammoAmmount > 0)
            {
                gunClass.activeAmmo.RemoveAmmo(1);
                fireRateTimer = 0;
                GameObject bullet = Instantiate(activeAmmo.bulletPrefab, shootPoint.transform.position, Quaternion.identity);
                bullet.transform.parent = null;

                Rigidbody bulletRb = bullet.AddComponent<Rigidbody>();
                bulletRb.useGravity = false;

                Vector3 bulletDir = new Vector3(0, Random.Range(-activeAmmo.bulletSpread, activeAmmo.bulletSpread), 0);
                bulletRb.velocity = transform.forward * activeAmmo.bulletSpeed + bulletDir;


                Vector3 vel = bulletRb.velocity;

                bullet.transform.rotation = Quaternion.LookRotation(vel);
                bulletRb.constraints = RigidbodyConstraints.FreezeRotation;

                Destroy(bullet, activeAmmo.bulletLifeTime);
            }
        }
    }

}
