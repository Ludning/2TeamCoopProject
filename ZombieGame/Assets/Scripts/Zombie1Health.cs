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

    public void Setup(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    protected override void OnEnable()
    {
        minTimeBetDamaged = 0;
        base.OnEnable();
    }


    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;
        Debug.Log("좀비 " + damageMessage.damage + " 피해입음");
        return true;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("좀비 죽음");
        animator.SetTrigger("Die");

    }

    public void Disappear()
    {
        //gameObject.SetActive(false);
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}
