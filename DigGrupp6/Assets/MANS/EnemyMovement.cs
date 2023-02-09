using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemyMain;
    private float attackTimer;
    private float sizeX;
    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        sizeX = transform.localScale.x;
        enemyMain = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = (transform.position - enemyMain.player.transform.position).sqrMagnitude;

        if (!isAttacking)
        {
            attackTimer -= Time.deltaTime;
        }

        //Sees player
        if (playerDistance <= enemyMain.sightDistance * enemyMain.sightDistance)
        {
            enemyMain.anim.SetBool("Walking", true);
            Move();
        }
        else //Idle
        {
            enemyMain.anim.SetBool("Walking", false);
            ChangeVelocity(0, enemyMain.deAccelerationTime);
        }

        //Attack
        if (playerDistance <= enemyMain.attackDistance 
            && attackTimer <= 0
            && !enemyMain.player.IsHidden)
        {
            StartCoroutine(Attack());
        }
    }

    void Move()
    {
        float movement = Mathf.Sign(enemyMain.player.transform.position.x - transform.position.x);

        ChangeVelocity(enemyMain.movementSpeed * movement, enemyMain.accelerationTime);
    }

    void ChangeVelocity(float newVelocity, float acc)
    {
        Vector3 velocity = enemyMain.rb.velocity;
        velocity.z = 0;
        velocity.x = Mathf.Lerp(velocity.x, newVelocity, acc * Time.deltaTime);

        if (Mathf.Abs(velocity.x) >= 0.1f)
        {
            transform.localScale = new Vector3(sizeX * Mathf.Sign(velocity.x), transform.localScale.y, transform.localScale.z);
        }

        enemyMain.rb.velocity = velocity;
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        enemyMain.anim.SetTrigger("Clap");
        print("Clap");
        attackTimer = enemyMain.attackCooldown * Random.Range(0.8f, 1.2f);

        yield return new WaitForSeconds(enemyMain.attackWindupDuration);

        enemyMain.damageCollider.enabled = true;

        yield return new WaitForSeconds(enemyMain.attackDamageDuration);

        enemyMain.damageCollider.enabled = false;
        isAttacking = false;
    }
}
