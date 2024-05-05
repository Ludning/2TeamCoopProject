using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void UpdateHpUI()
    {

    }

    protected override void OnEnable()
    {
        //maxHealth = 100f;
        base.OnEnable();
        UpdateHpUI();
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        UpdateHpUI();
    }

    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if(!base.ApplyDamage(damageMessage)) return false;
        Debug.Log(damageMessage.damage + "의 데미지 받음");
        UpdateHpUI();
        return true;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("플레이어 죽음");
        UpdateHpUI();

        //animator.SetTrigger("Die");
    }
}
