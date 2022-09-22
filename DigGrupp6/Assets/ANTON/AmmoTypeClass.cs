using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new GunClass", menuName = "Scriptable Objects/GunClass")]
public class AmmoTypeClass : ScriptableObject
{
    [SerializeField] string AmmoName;
    public int ammoAmmount;
    public Sprite ammoSprite;

    public GameObject bulletPrefab;
    public int bulletAmmount;
    public float bulletSpeed;
    public float bulletLifeTime;
    public float fireRate;
    public float bulletSpread;

    public void AddAmmo(int amt)
    {
        ammoAmmount += amt;
    }

    public void RemoveAmmo(int amt)
    {
        ammoAmmount -= amt;
    }
}
