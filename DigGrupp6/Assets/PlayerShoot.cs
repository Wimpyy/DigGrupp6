using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float shootSpeed;
    [SerializeField] float bulletDistance;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;

    private void Update()
    {
        LookAtMouse();
    }

    void LookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition + new Vector3(0, 0, 1);
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mouseToWorld.z = transform.position.z;

        transform.LookAt(mouseToWorld);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        
    }
}
