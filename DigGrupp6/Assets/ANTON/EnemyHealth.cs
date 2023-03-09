using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHp;
    [SerializeField] float hitEffectDuration;
    float currentHp;

    [SerializeField] Animator anim;
    PlayerShoot shooter;
    AmmoTypeClass currentAmmo;


    private void Awake()
    {
        currentHp = maxHp;
        shooter = FindObjectOfType<PlayerShoot>();
    }

    private void Update()
    {
        if (currentAmmo != shooter.gunClass.activeAmmo)
        {
            currentAmmo = shooter.gunClass.activeAmmo;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(GetHit(currentAmmo.knockback, currentAmmo.damage));
        }
    }

    IEnumerator GetHit(float knockback, float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }

        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(hitEffectDuration);

        Debug.Log("after wait");
    }
}
