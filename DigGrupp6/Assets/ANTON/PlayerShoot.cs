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
    [SerializeField] GameObject yoinkerObject;
    [SerializeField] float yoinkFovAdd;

    [HideInInspector]
    public GunClass gunClass;
    float fireRateTimer;

    float baseFov;
    GameObject baseFollowTarget;


    private void Start()
    {
        gunClass = FindObjectOfType<GunClass>();

        baseFov = cam.m_Lens.FieldOfView;
        baseFollowTarget = cam.Follow.gameObject;
        yoinkerObject.SetActive(false);
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;
        LookAtMouse();
        if (Input.GetMouseButton(1))
        {

            gunClass.RefreshUI();
            yoinkerObject.SetActive(true);
            if (cam.m_Lens.FieldOfView > (baseFov + yoinkFovAdd))
            {
                cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, baseFov + yoinkFovAdd, Time.deltaTime * 3f);
                cam.Follow = yoinkerObject.transform;
            }
        }
        else
        {

            gunClass.RefreshUI();
            yoinkerObject.SetActive(false);
            if (cam.m_Lens.FieldOfView < baseFov)
            {
                cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, baseFov, Time.deltaTime * 3f);
                cam.Follow = baseFollowTarget.transform;
            }
        }

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
