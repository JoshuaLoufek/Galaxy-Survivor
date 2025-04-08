using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{
    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;
    [SerializeField] Vector2 whipAttackSize = new Vector2(4f, 2f);

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < currentWeaponStats.extraAttacks; i++)
        {
            if (playerController.lastHorizontalVector > 0)
            {
                rightWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSize, 0f);
                DamageEnemy(colliders);
            }
            else
            {
                leftWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSize, 0f);
                DamageEnemy(colliders);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void DamageEnemy(Collider2D[] colliders)
    {
        for(int i = 0; i < colliders.Length; i++)
        {
            // Verifies the collider is an enemy, then deals damage to them
            BasicEnemyHealth enemy = colliders[i].GetComponent<BasicEnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(currentWeaponStats.damage);
                PostDamage((int)currentWeaponStats.damage, enemy.transform.position);
            }
        }
    }
}
