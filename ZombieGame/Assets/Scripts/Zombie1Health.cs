using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1Health : LivingEntity
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        //maxHealth = 100f;
        minTimeBetDamaged = 0;
        base.OnEnable();
    }

    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;

        return true;
    }

    public override void Die()
    {
        base.Die();

        animator.SetTrigger("Die");

        gameObject.SetActive(false);
    }
}
